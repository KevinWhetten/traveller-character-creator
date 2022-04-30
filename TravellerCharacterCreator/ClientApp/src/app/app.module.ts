import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {MatTooltipModule} from '@angular/material/tooltip';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import { CharacterCreatorComponent } from './character-creator/character-creator.component';
import { BackgroundSkillsComponent } from './character-creator/background-skills/background-skills.component';
import { CareersComponent } from './character-creator/careers/careers.component';
import { DraftComponent } from './character-creator/careers/draft/draft.component';
import { DrifterComponent } from './character-creator/careers/drifter/drifter.component';
import { PrisonComponent } from './character-creator/careers/prison/prison.component';
import { AgentComponent } from './character-creator/careers/agent/agent.component';
import { ArmyComponent } from './character-creator/careers/army/army.component';
import { CitizenComponent } from './character-creator/careers/citizen/citizen.component';
import { EntertainerComponent } from './character-creator/careers/entertainer/entertainer.component';
import { MarineComponent } from './character-creator/careers/marine/marine.component';
import { MerchantComponent } from './character-creator/careers/merchant/merchant.component';
import { NavyComponent } from './character-creator/careers/navy/navy.component';
import { NobleComponent } from './character-creator/careers/noble/noble.component';
import { RogueComponent } from './character-creator/careers/rogue/rogue.component';
import { ScholarComponent } from './character-creator/careers/scholar/scholar.component';
import { ScoutComponent } from './character-creator/careers/scout/scout.component';
import { PsionComponent } from './character-creator/careers/psion/psion.component';
import { CharacteristicsComponent } from './character-creator/characteristics/characteristics.component';
import { EducationComponent } from './character-creator/education/education.component';
import { EducationEventComponent } from './character-creator/education/education-event/education-event.component';
import { EducationLifeEventComponent } from './character-creator/education/education-event/education-life-event/education-life-event.component';
import { MilitaryAcademyComponent } from './character-creator/education/military-academy/military-academy.component';
import { MilitaryAcademyEventComponent } from './character-creator/education/military-academy/military-academy-event/military-academy-event.component';
import { MilitaryAcademyGraduationComponent } from './character-creator/education/military-academy/military-academy-graduation/military-academy-graduation.component';
import { UniversityComponent } from './character-creator/education/university/university.component';
import { UniversityEventComponent } from './character-creator/education/university/university-event/university-event.component';
import { UniversityGraduationComponent } from './character-creator/education/university/university-graduation/university-graduation.component';
import { UniversityLifeEventComponent } from './character-creator/education/university/university-event/university-life-event/university-life-event.component';
import { MilitaryAcademyLifeEventComponent } from './character-creator/education/military-academy/military-academy-event/military-academy-life-event/military-academy-life-event.component';
import { EducationPsionicTestComponent } from './character-creator/education/education-event/education-psionic-test/education-psionic-test.component';
import { MilitaryAcademyPsionicTestComponent } from './character-creator/education/military-academy/military-academy-event/military-academy-psionic-test/military-academy-psionic-test.component';
import { UniversityPsionicTestComponent } from './character-creator/education/university/university-event/university-psionic-test/university-psionic-test.component';
import { UniversitySkillsComponent } from './character-creator/education/university/university-skills/university-skills.component';
import { FinalStepsComponent } from './character-creator/final-steps/final-steps.component';
import { CharacterSheetComponent } from './character-sheet/character-sheet.component';
import { SkillSelectComponent } from './controls/skill-select/skill-select.component';
import { PsionicTestComponent } from './character-creator/careers/psionic-test/psionic-test.component';
import { LifeEventComponent } from './character-creator/careers/life-events/life-event.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CharacterCreatorComponent,
    BackgroundSkillsComponent,
    CareersComponent,
    DraftComponent,
    DrifterComponent,
    PrisonComponent,
    AgentComponent,
    ArmyComponent,
    CitizenComponent,
    EntertainerComponent,
    MarineComponent,
    MerchantComponent,
    NavyComponent,
    NobleComponent,
    RogueComponent,
    ScholarComponent,
    ScoutComponent,
    PsionComponent,
    CharacteristicsComponent,
    EducationComponent,
    EducationEventComponent,
    EducationLifeEventComponent,
    MilitaryAcademyComponent,
    MilitaryAcademyEventComponent,
    MilitaryAcademyGraduationComponent,
    UniversityComponent,
    UniversityEventComponent,
    UniversityGraduationComponent,
    UniversityLifeEventComponent,
    MilitaryAcademyLifeEventComponent,
    EducationPsionicTestComponent,
    MilitaryAcademyPsionicTestComponent,
    UniversityPsionicTestComponent,
    UniversitySkillsComponent,
    FinalStepsComponent,
    CharacterSheetComponent,
    SkillSelectComponent,
    PsionicTestComponent,
    LifeEventComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    MatTooltipModule,
    RouterModule.forRoot([
      {path: 'character-sheet', component: CharacterSheetComponent},
      {path: 'character-creator', component: CharacterCreatorComponent},
      {path: 'character-creator/characteristics', component: CharacteristicsComponent},
      {path: 'character-creator/background-skills', component: BackgroundSkillsComponent},
      {path: 'character-creator/education', component: EducationComponent},
      {path: 'character-creator/education/university', component: UniversityComponent},
      {path: 'character-creator/education/university/skills', component: UniversitySkillsComponent},
      {path: 'character-creator/education/university/event', component: UniversityEventComponent},
      {path: 'character-creator/education/university/graduate', component: UniversityGraduationComponent},
      {path: 'character-creator/education/university/psionic-test', component: UniversityPsionicTestComponent},
      {path: 'character-creator/education/university/life-event', component: UniversityLifeEventComponent},
      {path: 'character-creator/education/military-academy', component: MilitaryAcademyComponent},
      {path: 'character-creator/education/military-academy/event', component: MilitaryAcademyEventComponent},
      {path: 'character-creator/education/military-academy/graduate', component: MilitaryAcademyGraduationComponent},
      {path: 'character-creator/education/military-academy/psionic-test', component: UniversityPsionicTestComponent},
      {path: 'character-creator/education/military-academy/life-event', component: UniversityLifeEventComponent},
      {path: 'character-creator/careers', component: CareersComponent},
      {path: 'character-creator/careers/draft', component: DraftComponent},
      {path: 'character-creator/careers/drifter', component: DrifterComponent},
      {path: 'character-creator/careers/prison', component: PrisonComponent},
      {path: 'character-creator/final-steps', component: FinalStepsComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
