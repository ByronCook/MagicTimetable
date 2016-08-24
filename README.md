# MagicTimetable
Timetable organizer for festivals

Currently reading all data from a google spreadsheet document (in .tsv) (tab seperated values)
Unfortunately because of the free version of Xamarin, not able to include the JSON library to serialize data (would make the application size too big to be able to create an .apk)
Therefore I was forced to use the cheaphax solution of reading the document on nearly every activity.

All activity layouts are made dynamically depending on the result coming back from the google spreadsheet allowing for easy updating of data.
