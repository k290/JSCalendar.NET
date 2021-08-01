# JSCalendar.NET
A fluent .NET implementaiton of the JSCalendar spec. The spec intends to supersede the iCal spec. This project aims to provide a sustainable, robust and performant solution to build, serialize and deserialize JSCalendar compliant JSON.  The library encourages strict conformance to the spec. Whenever possible validation exceptions will thrown when non-conformant JSON is serialized or deserialized. We believe in helping developers fall into the pit of success.

https://datatracker.ietf.org/doc/html/rfc8984

A brief overview can be found here: https://devguide.calconnect.org/jsCalendar-Topics/introduction/
## Vision
If this project is viable, the goal is to make it sustainable over the long-term. Having more than one maintainer is in encouraged. Contributions from the community is encouraged. I am open to comments, suggestions and criticisms for any aspect of this project.

## Current state
This project is in an experimental pre-alpha state.

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

[![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg
