import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModules } from './material.modules';
import { GameInfoComponent } from './components/game-info/game-info.component';
import { ScoreboardComponent } from './components/scoreboard/scoreboard.component';
import { EmpireInfoComponent } from './components/empire-info/empire-info.component';
import { EmpireArmyComponent } from './components/empire-army/empire-army.component';
import { DragonInfoComponent } from './components/dragon-info/dragon-info.component';
import { CombatArmyComponent } from './components/combat-army/combat-army.component';
import { CombatInfoComponent } from './components/combat-info/combat-info.component';
import { CombatReportsComponent } from './components/combat-reports/combat-reports.component';
import { CombatOverviewComponent } from './components/combat-overview/combat-overview.component';
import { EmpireOverviewComponent } from './components/empire-overview/empire-overview.component';
import { LoopingRhumbusesSpinnerModule, RadarSpinnerModule } from 'angular-epic-spinners';
import { LoadingComponent } from './components/loading/loading.component';
import { EmpireStructuresComponent } from './components/empire-structures/empire-structures.component';

@NgModule({
  declarations: [
    AppComponent,
    GameInfoComponent,
    ScoreboardComponent,
    EmpireInfoComponent,
    EmpireArmyComponent,
    DragonInfoComponent,
    CombatArmyComponent,
    CombatInfoComponent,
    CombatReportsComponent,
    CombatOverviewComponent,
    EmpireOverviewComponent,
    LoadingComponent,
    EmpireStructuresComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ...MaterialModules,
    LoopingRhumbusesSpinnerModule,
    RadarSpinnerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
