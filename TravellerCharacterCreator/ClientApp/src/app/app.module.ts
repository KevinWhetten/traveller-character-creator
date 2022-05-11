import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations'
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatCheckboxModule} from "@angular/material/checkbox";

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import { CharacterCreatorComponent } from './character-creator/character-creator.component';
import { BackgroundSkillsComponent } from './character-creator/background-skills/background-skills.component';
import { CareersComponent } from './character-creator/careers/careers.component';
import { CharacteristicsComponent } from './character-creator/characteristics/characteristics.component';
import { EducationComponent } from './character-creator/education/education.component';
import { EducationEventComponent } from './character-creator/events/education-event/education-event.component';
import { EducationLifeEventComponent } from './character-creator/events/education-event/education-life-event/education-life-event.component';
import { MilitaryAcademyComponent } from './character-creator/education/military-academy/military-academy.component';
import { MilitaryAcademyEventComponent } from './character-creator/education/military-academy/military-academy-event/military-academy-event.component';
import { MilitaryAcademyGraduationComponent } from './character-creator/education/military-academy/military-academy-graduation/military-academy-graduation.component';
import { UniversityComponent } from './character-creator/education/university/university.component';
import { UniversityEventComponent } from './character-creator/education/university/university-event/university-event.component';
import { UniversityGraduationComponent } from './character-creator/education/university/university-graduation/university-graduation.component';
import { UniversityLifeEventComponent } from './character-creator/education/university/university-event/university-life-event/university-life-event.component';
import { MilitaryAcademyLifeEventComponent } from './character-creator/education/military-academy/military-academy-event/military-academy-life-event/military-academy-life-event.component';
import { EducationPsionicTestComponent } from './character-creator/events/education-event/education-psionic-test/education-psionic-test.component';
import { MilitaryAcademyPsionicTestComponent } from './character-creator/education/military-academy/military-academy-event/military-academy-psionic-test/military-academy-psionic-test.component';
import { UniversityPsionicTestComponent } from './character-creator/education/university/university-event/university-psionic-test/university-psionic-test.component';
import { UniversitySkillsComponent } from './character-creator/education/university/university-skills/university-skills.component';
import { FinalStepsComponent } from './character-creator/final-steps/final-steps.component';
import { CharacterSheetComponent } from './character-sheet/character-sheet.component';
import { SkillSelectComponent } from './controls/skill-select/skill-select.component';
import { BasicInfoComponent } from './character-creator/basic-info/basic-info.component';
import { SkillCheckboxSelectComponent } from './controls/skill-checkbox-select/skill-checkbox-select.component';
import { EventPsionicsComponent} from "./character-creator/events/education-event/event-psionics/event-psionics.component";
import { EventRecognitionComponent } from './character-creator/events/education-event/event-recognition/event-recognition.component';
import {  EventTragedyComponent} from "./character-creator/events/education-event/event-tragedy/event-tragedy.component";
import {EventPrankComponent} from "./character-creator/events/education-event/event-prank/event-prank.component";
import {EventPartyComponent} from "./character-creator/events/education-event/event-party/event-party.component";
import {  EventFriendsComponent} from "./character-creator/events/education-event/event-friends/event-friends.component";
import {  EventPoliticalComponent} from "./character-creator/events/education-event/event-political/event-political.component";
import {EventHobbyComponent} from "./character-creator/events/education-event/event-hobby/event-hobby.component";
import {EventTutorComponent} from "./character-creator/events/education-event/event-tutor/event-tutor.component";
import {EventDraftComponent} from "./character-creator/events/education-event/event-draft/event-draft.component";
import { LifeEventComponent } from './character-creator/events/life-event/life-event.component';
import { PsionicTestComponent } from './character-creator/events/psionic-test/psionic-test.component';
import { CareerDescriptionComponent } from './character-creator/careers/career-description/career-description.component';
import { CareerMishapsComponent } from './character-creator/careers/career-mishaps/career-mishaps.component';
import {CareerBenefitsComponent} from "./character-creator/careers/career-benefits/career-benefits.component";
import {CareerEventsComponent} from "./character-creator/careers/career-events/career-events.component";
import {CareerProgressComponent} from "./character-creator/careers/career-progress/career-progress.component";
import {CareerSkillsComponent} from "./character-creator/careers/career-skills/career-skills.component";
import { CareerRanksComponent } from './character-creator/careers/career-ranks/career-ranks.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CharacterCreatorComponent,
    BackgroundSkillsComponent,
    CareersComponent,
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
    BasicInfoComponent,
    SkillCheckboxSelectComponent,
    EventPsionicsComponent,
    EventTragedyComponent,
    EventPrankComponent,
    EventPartyComponent,
    EventFriendsComponent,
    EventPoliticalComponent,
    EventHobbyComponent,
    EventTutorComponent,
    EventDraftComponent,
    EventRecognitionComponent,
    LifeEventComponent,
    PsionicTestComponent,
    CareerBenefitsComponent,
    CareerEventsComponent,
    CareerMishapsComponent,
    CareerProgressComponent,
    CareerSkillsComponent,
    CareerDescriptionComponent,
    CareerMishapsComponent,
    CareerRanksComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MatTooltipModule,
    RouterModule.forRoot([
      {path: 'character-sheet', component: CharacterSheetComponent},
      {path: 'character-creator', component: CharacterCreatorComponent},
      {path: 'character-creator/basic-info', component: BasicInfoComponent},
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
      {path: 'character-creator/final-steps', component: FinalStepsComponent}
    ], {
      anchorScrolling: 'enabled'
    }),
    MatCheckboxModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
