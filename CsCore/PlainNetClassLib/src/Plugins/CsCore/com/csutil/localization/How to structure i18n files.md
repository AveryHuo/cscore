# How to structure i18n files
From https://stackoverflow.com/a/10364346/165106

I've found that the best overall strategy is to somewhat reproduce the file structure so that given any translation, I can immediately find where it was called from. This gives me some kind of context for making the translation.

The majority of application translations are found in views, so my biggest top level namespace is usually `views`.

I create sub namespaces for the controller name and the action name or partial being used ex :

- `views.users.index.title`
- `views.articles._sidebar.header`

Both of these examples should make it obvious what part of my app we're translating and which file to look in to find it.

You mention navigation and buttons, if they are to be generic, then they belong in the `views.application` namespace just as do their view counterparts :

- `views.application._main_nav.links.about_us` - a link in our app's main navigation partial
- `views.application.buttons.save`
- `views.application.buttons.create` - I have a bunch of these buttons ready to be used when needed


Flash messages are generated from the controller, so their top level namespace is... `controllers`! :)

We apply the same logic as we do to views :

- `controllers.users.create.flash.success|alert|notice`

Again if you wanted to provide generic flash messages like "Operation successful", you would write something like this :

- `controllers.application.create.flash.notice`


Finally, keys can be anything that is valid YAML, but please stick to using periods `.` as separators and underscores `_` between words as a matter of convention.

The only thing left to sort out now, is getting rails' translations into its own namespace to clean up our top level :)


# One extended example file 
From https://github.com/svenfuchs/rails-i18n/blob/master/rails/locale/en.yml

```
---
en:
  activerecord:
    errors:
      messages:
        record_invalid: 'Validation failed: %{errors}'
        restrict_dependent_destroy:
          has_one: Cannot delete record because a dependent %{record} exists
          has_many: Cannot delete record because dependent %{record} exist
  date:
    abbr_day_names:
    - Sun
    - Mon
    - Tue
    - Wed
    - Thu
    - Fri
    - Sat
    abbr_month_names:
    - 
    - Jan
    - Feb
    - Mar
    - Apr
    - May
    - Jun
    - Jul
    - Aug
    - Sep
    - Oct
    - Nov
    - Dec
    day_names:
    - Sunday
    - Monday
    - Tuesday
    - Wednesday
    - Thursday
    - Friday
    - Saturday
    formats:
      default: "%Y-%m-%d"
      long: "%B %d, %Y"
      short: "%b %d"
    month_names:
    - 
    - January
    - February
    - March
    - April
    - May
    - June
    - July
    - August
    - September
    - October
    - November
    - December
    order:
    - :year
    - :month
    - :day
  datetime:
    distance_in_words:
      about_x_hours:
        one: about 1 hour
        other: about %{count} hours
      about_x_months:
        one: about 1 month
        other: about %{count} months
      about_x_years:
        one: about 1 year
        other: about %{count} years
      almost_x_years:
        one: almost 1 year
        other: almost %{count} years
      half_a_minute: half a minute
      less_than_x_seconds:
        one: less than 1 second
        other: less than %{count} seconds
      less_than_x_minutes:
        one: less than a minute
        other: less than %{count} minutes
      over_x_years:
        one: over 1 year
        other: over %{count} years
      x_seconds:
        one: 1 second
        other: "%{count} seconds"
      x_minutes:
        one: 1 minute
        other: "%{count} minutes"
      x_days:
        one: 1 day
        other: "%{count} days"
      x_months:
        one: 1 month
        other: "%{count} months"
      x_years:
        one: 1 year
        other: "%{count} years"
    prompts:
      second: Second
      minute: Minute
      hour: Hour
      day: Day
      month: Month
      year: Year
  errors:
    format: "%{attribute} %{message}"
    messages:
      accepted: must be accepted
      blank: can't be blank
      confirmation: doesn't match %{attribute}
      empty: can't be empty
      equal_to: must be equal to %{count}
      even: must be even
      exclusion: is reserved
      greater_than: must be greater than %{count}
      greater_than_or_equal_to: must be greater than or equal to %{count}
      inclusion: is not included in the list
      invalid: is invalid
      less_than: must be less than %{count}
      less_than_or_equal_to: must be less than or equal to %{count}
      model_invalid: 'Validation failed: %{errors}'
      not_a_number: is not a number
      not_an_integer: must be an integer
      odd: must be odd
      other_than: must be other than %{count}
      present: must be blank
      required: must exist
      taken: has already been taken
      too_long:
        one: is too long (maximum is 1 character)
        other: is too long (maximum is %{count} characters)
      too_short:
        one: is too short (minimum is 1 character)
        other: is too short (minimum is %{count} characters)
      wrong_length:
        one: is the wrong length (should be 1 character)
        other: is the wrong length (should be %{count} characters)
    template:
      body: 'There were problems with the following fields:'
      header:
        one: 1 error prohibited this %{model} from being saved
        other: "%{count} errors prohibited this %{model} from being saved"
  helpers:
    select:
      prompt: Please select
    submit:
      create: Create %{model}
      submit: Save %{model}
      update: Update %{model}
  number:
    currency:
      format:
        delimiter: ","
        format: "%u%n"
        precision: 2
        separator: "."
        significant: false
        strip_insignificant_zeros: false
        unit: "$"
    format:
      delimiter: ","
      precision: 3
      separator: "."
      significant: false
      strip_insignificant_zeros: false
    human:
      decimal_units:
        format: "%n %u"
        units:
          billion: Billion
          million: Million
          quadrillion: Quadrillion
          thousand: Thousand
          trillion: Trillion
          unit: ''
      format:
        delimiter: ''
        precision: 3
        significant: true
        strip_insignificant_zeros: true
      storage_units:
        format: "%n %u"
        units:
          byte:
            one: Byte
            other: Bytes
          eb: EB
          gb: GB
          kb: KB
          mb: MB
          pb: PB
          tb: TB
    percentage:
      format:
        delimiter: ''
        format: "%n%"
    precision:
      format:
        delimiter: ''
  support:
    array:
      last_word_connector: ", and "
      two_words_connector: " and "
      words_connector: ", "
  time:
    am: am
    formats:
      default: "%a, %d %b %Y %H:%M:%S %z"
      long: "%B %d, %Y %H:%M"
      short: "%d %b %H:%M"
    pm: pm
```