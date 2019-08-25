﻿using com.csutil;
using com.csutil.testing;
using Mopsicus.InfiniteScroll;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using com.csutil.tests;

public class XunitTestRunnerUi : MonoBehaviour {

    public int defaultEntryHeight = 160;
    public int timeoutInMs = 30000;
    public bool autoRunAllTests = false;

    private List<XunitTestRunner.Test> allTests;
    private InfiniteScroll listUi;

    private void OnEnable() {
        var links = gameObject.GetLinkMap();
        listUi = links.Get<InfiniteScroll>("HorizontalScrollView");
        listUi.OnHeight += GetHeightOfEachViewEntry;
        listUi.OnFill += FillViewWithModelEntry;
        links.Get<Button>("StartButton").SetOnClickAction((button) => {
            StartCoroutine(RunAllTests(listUi));
            links.Get<Text>("ButtonText").text = "Now running " + allTests.Count + " tests..";
        });
    }

    private int GetHeightOfEachViewEntry(int index) { return defaultEntryHeight; }

    private void FillViewWithModelEntry(int pos, GameObject view) {
        var test = allTests[pos];
        var links = view.GetLinkMap();
        links.Get<Button>("EntryBackground").SetOnClickAction(delegate {
            MainThread.instance.StartCoroutine(RunTest(test, listUi));
        });
        links.Get<Text>("Name").text = test.methodToTest.ToStringV2();
        if (test.testTask == null) {
            links.Get<Text>("Status").text = "Not started yet";
            links.Get<Image>("EntryBackground").color = Color.white;
        } else if (test.testTask.IsFaulted) {
            var error = test.testTask.Exception;
            links.Get<Text>("Status").text = "Error: " + error;
            links.Get<Image>("EntryBackground").color = Color.red;
        } else if (test.testTask.IsCompleted) {
            links.Get<Text>("Status").text = "Passed";
            links.Get<Image>("EntryBackground").color = Color.green;
        } else {
            links.Get<Text>("Status").text = "Running..";
            links.Get<Image>("EntryBackground").color = Color.blue;
        }
    }

    public IEnumerator RunAllTests(InfiniteScroll listUi) {
        var allClasses = typeof(MathTests).Assembly.GetExportedTypes();
        allTests = XunitTestRunner.CollectAllTests(allClasses, delegate {
            //// setup before each test, use same error collector for all tests:
        });
        AssertV2.AreNotEqual(0, allTests.Count);
        listUi.InitData(allTests.Count);
        if (autoRunAllTests) {
            foreach (var test in allTests) { yield return RunTest(test, listUi); }
            AssertV2.AreEqual(0, allTests.Filter(t => t.testTask.IsFaulted).Count());
        }
    }

    private IEnumerator RunTest(XunitTestRunner.Test test, InfiniteScroll listUi) {
        test.StartTest();
        yield return new WaitForEndOfFrame();
        listUi.UpdateVisible();
        yield return test.testTask.AsCoroutine((e) => { Debug.LogError(e); }, timeoutInMs);
        yield return new WaitForEndOfFrame();
        listUi.UpdateVisible();
    }
}