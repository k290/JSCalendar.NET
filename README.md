# JSCalendar.NET [![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

A fluent .NET implementaiton of the IETF JSCalendar spec. The spec is intended to supersede the iCal spec. The reasons for this can be found in the spec itself. A brief overview can be found here: https://devguide.calconnect.org/jsCalendar-Topics/introduction/

The spec: https://datatracker.ietf.org/doc/html/rfc8984
Errata: https://www.rfc-editor.org/errata_search.php?rfc=8984
Status: https://datatracker.ietf.org/doc/rfc8984/

A list of properties in the spec: https://www.iana.org/assignments/jscalendar/jscalendar.xhtml

Converting to and from ical: https://www.ietf.org/id/draft-ietf-calext-jscalendar-icalendar-06.html

This .NET library aims to provide a sustainable, robust and performant solution to build, serialize and deserialize JSCalendar compliant JSON.  Whenever possible, the library encourages strict conformance to the spec.

## Vision
If this project is viable, the goal is to make it sustainable over the long-term. Having more than one maintainer is in encouraged. Contributions from the community is encouraged. I am open to comments, suggestions and criticisms for any aspect of this project.

## Current state
This project is in an experimental pre-alpha state. This means it is under initial ideation and development. The current focus is feature-complete serialization to a JSCalendar JSON string. 

## Sample
```
            var builtObject = await new JSGroupBuilder()
                                 .WithUid("Group01")
                                 .WithRelatedTo("SomeId", r => r.WithRelation(RelationType.Parent).WithRelation(RelationType.Child))
                                 .WithRelatedTo("SomeOtherId", r => r.WithRelation(RelationType.Next))
                                 .WithEvent(e => e.WithUid("E1"))
                                 .WithEvent(e => e.WithUid("E2"))
                                 .WithTask(t => t.WithUid("T1"))
                          .BuildAsync();

            var result = await builtObject.GetJsonAsync();
```
# License

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg
