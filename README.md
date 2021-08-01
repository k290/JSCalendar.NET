# JSCalendar.NET
A .fluent NET implementaiton of the JSCalendar Spec

https://datatracker.ietf.org/doc/html/rfc8984

## Current state
This project is in pre-alpha, and under active development and user research.

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
