### Travelplanner

#### AddTrack
* Hoppar över

#### SelectTrain
* Exception
* Travelplanner.trainID
* Exception - two identical trains in the same planner? Move code?

#### StartAt
* Kontrollera att travelplanner innehåller ett station id och en time
* Kontrollera att station.ID existerar
* Lägg time i tryparse

#### ArriveAt
* Kontrollera att travelplanner innehåller ett station id och en time
* Kontrollera att station.ID existerar
* Lägg time i tryparse

#### ~GeneratePlan()~ - Vi struntar i denna, vi har testat med felaktigt api. Det blir ingen idiotsäker setup.
* Testa med api vi vet kraschar (testar även metod are stuff going to crash

### Travelplan - Inget att testa

### TrackOrm

#### ~LoadTrack~ Klar
* Kolla att contains stuff

#### ~FindStart~ Klar
* Kontrollera i Mocktrack

### TrackDescription - Låt vara för nu

### LevelCrossing - Låt vara för nu

### FileIO - Finns redan

### Clock - Låt vara för nu
