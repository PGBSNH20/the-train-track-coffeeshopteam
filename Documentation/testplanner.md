### Travelplanner

#### ~AddTrack~ -Tar travelplanner som argument - hoppa över
* Hoppar över

#### ~SelectTrain~
* Exception - check
* Travelplanner.trainID felhantering - check

#### ~StartAt~
* Kontrollera att travelplanner innehåller ett station id och en time
* Kontrollera att station.ID existerar -check
* Lägg time i tryparse -check

#### ~ArriveAt~
* Kontrollera att station.ID existerar -check
* Lägg time i tryparse -check

#### ~GeneratePlan()~ - Vi struntar i denna, vi har testat med felaktigt api. Det blir ingen idiotsäker setup.
* Testa med api vi vet kraschar (testar även metod are stuff going to crash

### Travelplan - Inget att testa

### TrackOrm

#### ~LoadTrack~ Klar
* Kolla att contains stuff -check

#### ~FindStart~ Klar
* Kontrollera i Mocktrack -check

### ~TrackDescription~ - Inget att testa

### FileIO - Finns redan

### ~Clock~ - Inga meningsfulla tester
