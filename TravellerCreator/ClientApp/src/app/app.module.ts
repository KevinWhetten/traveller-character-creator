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
import { BackgroundSkillsComponent } from './character-creator/components/background-skills/background-skills.component';
import { CareerSelectionComponent } from './character-creator/components/careers/career-selection/career-selection.component';
import { DetermineCharacteristicsComponent } from './character-creator/components/determine-characteristics/determine-characteristics.component';
import { EducationComponent } from './character-creator/components/education/education.component';
import { EducationEventComponent } from './character-creator/components/events/education-event/education-event.component';
import { MilitaryAcademyComponent } from './character-creator/components/education/military-academy/military-academy.component';
import { MilitaryAcademyEventComponent } from './character-creator/components/education/military-academy/military-academy-event/military-academy-event.component';
import { MilitaryAcademyGraduationComponent } from './character-creator/components/education/military-academy/military-academy-graduation/military-academy-graduation.component';
import { UniversityComponent } from './character-creator/components/education/university/university.component';
import { UniversityEventComponent } from './character-creator/components/education/university/university-event/university-event.component';
import { UniversityGraduationComponent } from './character-creator/components/education/university/university-graduation/university-graduation.component';
import { UniversitySkillsComponent } from './character-creator/components/education/university/university-skills/university-skills.component';
import { FinalStepsComponent } from './character-creator/components/final-steps/final-steps.component';
import { CharacterSheetComponent } from './character-creator/components/character-sheet/character-sheet.component';
import { CharacteristicsComponent } from './character-creator/components/character-sheet/characteristics/characteristics.component';
import { SkillSelectComponent } from './character-creator/controls/skill-select/skill-select.component';
import { BasicInfoComponent } from './character-creator/components/basic-info/basic-info.component';
import { SkillCheckboxSelectComponent } from './character-creator/controls/skill-checkbox-select/skill-checkbox-select.component';
import { EventPsionicsComponent} from "./character-creator/components/events/education-event/event-psionics/event-psionics.component";
import { EventRecognitionComponent } from './character-creator/components/events/education-event/event-recognition/event-recognition.component';
import {  EventTragedyComponent} from "./character-creator/components/events/education-event/event-tragedy/event-tragedy.component";
import {EventPrankComponent} from "./character-creator/components/events/education-event/event-prank/event-prank.component";
import {EventPartyComponent} from "./character-creator/components/events/education-event/event-party/event-party.component";
import {  EventFriendsComponent} from "./character-creator/components/events/education-event/event-friends/event-friends.component";
import {  EventPoliticalComponent} from "./character-creator/components/events/education-event/event-political/event-political.component";
import {EventHobbyComponent} from "./character-creator/components/events/education-event/event-hobby/event-hobby.component";
import {EventTutorComponent} from "./character-creator/components/events/education-event/event-tutor/event-tutor.component";
import {EventDraftComponent} from "./character-creator/components/events/education-event/event-draft/event-draft.component";
import { LifeEventComponent } from './character-creator/components/events/life-event/life-event.component';
import { PsionicTestComponent } from './character-creator/components/events/psionic-test/psionic-test.component';
import { CareerDescriptionComponent } from './character-creator/components/careers/career-selection/career-description/career-description.component';
import { CareerMishapsComponent } from './character-creator/components/careers/career-selection/career-mishaps/career-mishaps.component';
import {CareerBenefitsComponent} from "./character-creator/components/careers/career-selection/career-benefits/career-benefits.component";
import {CareerEventsComponent} from "./character-creator/components/careers/career-selection/career-events/career-events.component";
import {CareerProgressComponent} from "./character-creator/components/careers/career-selection/career-progress/career-progress.component";
import {CareerSkillsComponent} from "./character-creator/components/careers/career-selection/career-skills/career-skills.component";
import { CareerRanksComponent } from './character-creator/components/careers/career-selection/career-ranks/career-ranks.component';
import { CareerBasicTrainingComponent } from './character-creator/components/careers/career-basic-training/career-basic-training.component';
import { CareerQualificationComponent } from './character-creator/components/careers/career-qualification/career-qualification.component';
import { CareerAssignmentComponent } from './character-creator/components/careers/career-assignment/career-assignment.component';
import { CareerSkillGenerationComponent } from './character-creator/components/careers/career-skill-generation/career-skill-generation.component';
import { CareerSurvivalComponent } from './character-creator/components/careers/career-survival/career-survival.component';
import { CareerEventComponent } from './character-creator/components/careers/career-event/career-event.component';
import { CareerMishapComponent } from './character-creator/components/careers/career-mishap/career-mishap.component';
import { CareerCommissionComponent } from './character-creator/components/careers/career-commission/career-commission.component';
import { CareerAdvancementComponent } from './character-creator/components/careers/career-advancement/career-advancement.component';
import { CareerLeavingComponent } from './character-creator/components/careers/career-leaving/career-leaving.component';
import { DraftComponent } from './character-creator/components/careers/draft/draft.component';
import { CareerQualificationFailedComponent } from './character-creator/components/careers/career-qualification/career-qualification-failed/career-qualification-failed.component';
import { FirstCareerBasicTrainingComponent } from './character-creator/components/careers/career-basic-training/first-career-basic-training/first-career-basic-training.component';
import { MarineEventComponent } from './character-creator/components/events/marine-event/marine-event.component';
import { AgentEventComponent } from './character-creator/components/events/agent-event/agent-event.component';
import { ArmyEventComponent } from './character-creator/components/events/army-event/army-event.component';
import { CitizenEventComponent } from './character-creator/components/events/citizen-event/citizen-event.component';
import { DrifterEventComponent } from './character-creator/components/events/drifter-event/drifter-event.component';
import { EntertainerEventComponent } from './character-creator/components/events/entertainer-event/entertainer-event.component';
import { MerchantEventComponent } from './character-creator/components/events/merchant-event/merchant-event.component';
import { NavyEventComponent } from './character-creator/components/events/navy-event/navy-event.component';
import { NobleEventComponent } from './character-creator/components/events/noble-event/noble-event.component';
import { RogueEventComponent } from './character-creator/components/events/rogue-event/rogue-event.component';
import { ScholarEventComponent } from './character-creator/components/events/scholar-event/scholar-event.component';
import { ScoutEventComponent } from './character-creator/components/events/scout-event/scout-event.component';
import { PrisonerEventComponent } from './character-creator/components/events/prisoner-event/prisoner-event.component';
import { PsionEventComponent } from './character-creator/components/events/psion-event/psion-event.component';
import { AgentDisasterEventComponent } from './character-creator/components/events/agent-event/agent-disaster-event/agent-disaster-event.component';
import { AgentInvestigationEventComponent } from './character-creator/components/events/agent-event/agent-investigation-event/agent-investigation-event.component';
import { AgentMissionEventComponent } from './character-creator/components/events/agent-event/agent-mission-event/agent-mission-event.component';
import { AgentNetworkEventComponent } from './character-creator/components/events/agent-event/agent-network-event/agent-network-event.component';
import { AgentTrainingEventComponent } from './character-creator/components/events/agent-event/agent-training-event/agent-training-event.component';
import { AgentLifeEventComponent } from './character-creator/components/events/agent-event/agent-life-event/agent-life-event.component';
import { AgentUndercoverEventComponent } from './character-creator/components/events/agent-event/agent-undercover-event/agent-undercover-event.component';
import { AgentBeyondEventComponent } from './character-creator/components/events/agent-event/agent-beyond-event/agent-beyond-event.component';
import { AgentSpecialistEventComponent } from './character-creator/components/events/agent-event/agent-specialist-event/agent-specialist-event.component';
import { AgentSeniorEventComponent } from './character-creator/components/events/agent-event/agent-senior-event/agent-senior-event.component';
import { AgentConspiracyEventComponent } from './character-creator/components/events/agent-event/agent-conspiracy-event/agent-conspiracy-event.component';
import { AgentMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-mishap.component';
import {CareerMusterOutComponent} from "./character-creator/components/careers/career-muster-out/career-muster-out.component";
import { MarineMusterOutComponent } from './character-creator/components/careers/career-muster-out/marine-muster-out/marine-muster-out.component';
import { AgentMusterOutComponent } from './character-creator/components/careers/career-muster-out/agent-muster-out/agent-muster-out.component';
import { AlertComponent } from './controls/alert/alert.component';
import { AgentSeverelyInjuredMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-severely-injured-mishap/agent-severely-injured-mishap.component';
import { AgentDealMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-deal-mishap/agent-deal-mishap.component';
import { AgentInvestigationMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-investigation-mishap/agent-investigation-mishap.component';
import { AgentLearnMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-learn-mishap/agent-learn-mishap.component';
import { AgentHomeMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-home-mishap/agent-home-mishap.component';
import { AgentInjuredMishapComponent } from './character-creator/components/mishaps/agent-mishap/agent-injured-mishap/agent-injured-mishap.component';
import { SeverelyInjuredMishapComponent } from './character-creator/components/mishaps/severely-injured-mishap/severely-injured-mishap.component';
import { InjuredMishapComponent } from './character-creator/components/mishaps/injured-mishap/injured-mishap.component';
import { AgentHomeInjuryComponent } from './character-creator/components/mishaps/agent-mishap/agent-home-mishap/agent-home-injury/agent-home-injury.component';
import { SkillRollComponent } from './character-creator/controls/skill-roll/skill-roll.component';
import { CharacteristicRollComponent } from './character-creator/controls/characteristic-roll/characteristic-roll.component';
import { EventSicknessOrInjuryComponent } from './character-creator/components/events/life-event/event-sickness-or-injury/event-sickness-or-injury.component';
import { EventBirthOrDeathComponent } from './character-creator/components/events/life-event/event-birth-or-death/event-birth-or-death.component';
import { EventEndingOfRelationshipComponent } from './character-creator/components/events/life-event/event-ending-of-relationship/event-ending-of-relationship.component';
import { EventImprovedRelationshipComponent } from './character-creator/components/events/life-event/event-improved-relationship/event-improved-relationship.component';
import { EventNewRelationshipComponent } from './character-creator/components/events/life-event/event-new-relationship/event-new-relationship.component';
import { EventNewContactComponent } from './character-creator/components/events/life-event/event-new-contact/event-new-contact.component';
import { EventBetrayalComponent } from './character-creator/components/events/life-event/event-betrayal/event-betrayal.component';
import { EventTravelComponent } from './character-creator/components/events/life-event/event-travel/event-travel.component';
import { EventGoodFortuneComponent } from './character-creator/components/events/life-event/event-good-fortune/event-good-fortune.component';
import { EventCrimeComponent } from './character-creator/components/events/life-event/event-crime/event-crime.component';
import { EventUnusualEventComponent } from './character-creator/components/events/life-event/event-unusual-event/event-unusual-event.component';
import { UnusualEventPsionicsComponent } from './character-creator/components/events/life-event/event-unusual-event/unusual-event-psionics/unusual-event-psionics.component';
import { UnusualEventAliensComponent } from './character-creator/components/events/life-event/event-unusual-event/unusual-event-aliens/unusual-event-aliens.component';
import { UnusualEventAlienArtifactComponent } from './character-creator/components/events/life-event/event-unusual-event/unusual-event-alien-artifact/unusual-event-alien-artifact.component';
import { UnusualEventAmnesiaComponent } from './character-creator/components/events/life-event/event-unusual-event/unusual-event-amnesia/unusual-event-amnesia.component';
import { UnusualEventContactWithGovernmentComponent } from './character-creator/components/events/life-event/event-unusual-event/unusual-event-contact-with-government/unusual-event-contact-with-government.component';
import { UnusualEventAncientTechnologyComponent } from './character-creator/components/events/life-event/event-unusual-event/unusual-event-ancient-technology/unusual-event-ancient-technology.component';
import { StoryComponent } from './character-creator/controls/story/story.component';
import { RollComponent } from './character-creator/controls/roll/roll.component';
import { InjuryComponent } from './character-creator/components/mishaps/injury/injury.component';
import { InjuryNearlyKilledComponent } from './character-creator/components/mishaps/injury/injury-nearly-killed/injury-nearly-killed.component';
import { InjurySeverelyInjuredComponent } from './character-creator/components/mishaps/injury/injury-severely-injured/injury-severely-injured.component';
import { InjuryMissingEyeOrLimbComponent } from './character-creator/components/mishaps/injury/injury-missing-eye-or-limb/injury-missing-eye-or-limb.component';
import { InjuryScarredComponent } from './character-creator/components/mishaps/injury/injury-scarred/injury-scarred.component';
import { InjuryInjuredComponent } from './character-creator/components/mishaps/injury/injury-injured/injury-injured.component';
import { InjuryLightlyInjuredComponent } from './character-creator/components/mishaps/injury/injury-lightly-injured/injury-lightly-injured.component';
import { CitizenMishapComponent } from './character-creator/components/mishaps/citizen-mishap/citizen-mishap.component';
import { RogueMishapComponent } from './character-creator/components/mishaps/rogue-mishap/rogue-mishap.component';
import { InjuryTableComponent } from './character-creator/components/mishaps/injury/injury-table/injury-table.component';
import { SectorCreatorComponent } from './sector-creator/sector-creator.component';
import {MatDialogModule} from "@angular/material/dialog";
import {MongooseSubsectorComponent} from "./sector-creator/subsector/basic-subsector/mongoose-subsector.component";
import { StarFrontiersSubsectorComponent } from './sector-creator/subsector/star-frontiers-subsector/star-frontiers-subsector.component';
import { MongooseSectorComponent } from './sector-creator/sector/mongoose-sector/mongoose-sector.component';
import { StarFrontiersSectorComponent } from './sector-creator/sector/star-frontiers-sector/star-frontiers-sector.component';
import { RttWorldgenSectorComponent } from './sector-creator/sector/rttworldgen-sector/rttworldgen-sector.component';
import { RttWorldgenSubsectorComponent } from './sector-creator/subsector/rttworldgen-subsector/rttworldgen-subsector.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CharacterCreatorComponent,
        BackgroundSkillsComponent,
        CareerSelectionComponent,
        DetermineCharacteristicsComponent,
        EducationComponent,
        EducationEventComponent,
        MilitaryAcademyComponent,
        MilitaryAcademyEventComponent,
        MilitaryAcademyGraduationComponent,
        UniversityComponent,
        UniversityEventComponent,
        UniversityGraduationComponent,
        UniversitySkillsComponent,
        FinalStepsComponent,
        CharacterSheetComponent,
        CharacteristicsComponent,
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
        CareerRanksComponent,
        CareerBasicTrainingComponent,
        CareerBasicTrainingComponent,
        CareerQualificationComponent,
        CareerAssignmentComponent,
        CareerSkillGenerationComponent,
        CareerSurvivalComponent,
        CareerEventComponent,
        CareerMishapComponent,
        CareerCommissionComponent,
        CareerAdvancementComponent,
        CareerLeavingComponent,
        DraftComponent,
        CareerQualificationFailedComponent,
        FirstCareerBasicTrainingComponent,
        MarineEventComponent,
        AgentEventComponent,
        ArmyEventComponent,
        CitizenEventComponent,
        DrifterEventComponent,
        EntertainerEventComponent,
        MerchantEventComponent,
        NavyEventComponent,
        NobleEventComponent,
        RogueEventComponent,
        ScholarEventComponent,
        ScoutEventComponent,
        PrisonerEventComponent,
        PsionEventComponent,
        AgentDisasterEventComponent,
        AgentInvestigationEventComponent,
        AgentMissionEventComponent,
        AgentNetworkEventComponent,
        AgentTrainingEventComponent,
        AgentLifeEventComponent,
        AgentUndercoverEventComponent,
        AgentBeyondEventComponent,
        AgentSpecialistEventComponent,
        AgentSeniorEventComponent,
        AgentConspiracyEventComponent,
        AgentMishapComponent,
        CareerMusterOutComponent,
        MarineMusterOutComponent,
        AgentMusterOutComponent,
        AlertComponent,
        AgentSeverelyInjuredMishapComponent,
        AgentDealMishapComponent,
        AgentInvestigationMishapComponent,
        AgentLearnMishapComponent,
        AgentHomeMishapComponent,
        AgentInjuredMishapComponent,
        SeverelyInjuredMishapComponent,
        InjuredMishapComponent,
        AgentHomeInjuryComponent,
        SkillRollComponent,
        CharacteristicRollComponent,
        EventSicknessOrInjuryComponent,
        EventBirthOrDeathComponent,
        EventEndingOfRelationshipComponent,
        EventImprovedRelationshipComponent,
        EventNewRelationshipComponent,
        EventNewContactComponent,
        EventBetrayalComponent,
        EventTravelComponent,
        EventGoodFortuneComponent,
        EventCrimeComponent,
        EventUnusualEventComponent,
        UnusualEventPsionicsComponent,
        UnusualEventAliensComponent,
        UnusualEventAlienArtifactComponent,
        UnusualEventAmnesiaComponent,
        UnusualEventContactWithGovernmentComponent,
        UnusualEventAncientTechnologyComponent,
        StoryComponent,
        RollComponent,
        InjuryComponent,
        InjuryNearlyKilledComponent,
        InjurySeverelyInjuredComponent,
        InjuryMissingEyeOrLimbComponent,
        InjuryScarredComponent,
        InjuryInjuredComponent,
        InjuryLightlyInjuredComponent,
        CitizenMishapComponent,
        RogueMishapComponent,
        InjuryTableComponent,
        SectorCreatorComponent,
        MongooseSubsectorComponent,
        StarFrontiersSubsectorComponent,
        MongooseSectorComponent,
        StarFrontiersSectorComponent,
        RttWorldgenSectorComponent,
        RttWorldgenSubsectorComponent
    ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MatTooltipModule,
    MatDialogModule,
    RouterModule.forRoot([
      {path: 'character-sheet', component: CharacterSheetComponent},
      {path: 'character-creator', component: CharacterCreatorComponent},
      {path: 'character-creator/basic-info', component: BasicInfoComponent},
      {path: 'character-creator/characteristics', component: DetermineCharacteristicsComponent},
      {path: 'character-creator/background-skills', component: BackgroundSkillsComponent},
      {path: 'character-creator/education', component: EducationComponent},
      {path: 'character-creator/education/university', component: UniversityComponent},
      {path: 'character-creator/education/university/skills', component: UniversitySkillsComponent},
      {path: 'character-creator/education/university/event', component: UniversityEventComponent},
      {path: 'character-creator/education/university/graduate', component: UniversityGraduationComponent},
      {path: 'character-creator/education/military-academy', component: MilitaryAcademyComponent},
      {path: 'character-creator/education/military-academy/event', component: MilitaryAcademyEventComponent},
      {path: 'character-creator/education/military-academy/graduate', component: MilitaryAcademyGraduationComponent},
      {path: 'character-creator/components/careers', component: CareerSelectionComponent},
      {path: 'character-creator/components/careers/qualification', component: CareerQualificationComponent},
      {path: 'character-creator/components/careers/qualification/failed', component: CareerQualificationFailedComponent},
      {path: 'character-creator/components/careers/assignment', component: CareerAssignmentComponent},
      {path: 'character-creator/components/careers/basic-training', component: CareerBasicTrainingComponent},
      {path: 'character-creator/components/careers/basic-training/first-career', component: FirstCareerBasicTrainingComponent},
      {path: 'character-creator/components/careers/skill-generation', component: CareerSkillGenerationComponent},
      {path: 'character-creator/components/careers/survival', component: CareerSurvivalComponent},
      {path: 'character-creator/components/careers/event', component: CareerEventComponent},
      {path: 'character-creator/components/careers/agent/event', component: AgentEventComponent},
      {path: 'character-creator/components/careers/army/event', component: ArmyEventComponent},
      {path: 'character-creator/components/careers/citizen/event', component: CitizenEventComponent},
      {path: 'character-creator/components/careers/drifter/event', component: DrifterEventComponent},
      {path: 'character-creator/components/careers/entertainer/event', component: EntertainerEventComponent},
      {path: 'character-creator/components/careers/marine/event', component: MarineEventComponent},
      {path: 'character-creator/components/careers/merchant/event', component: MerchantEventComponent},
      {path: 'character-creator/components/careers/navy/event', component: NavyEventComponent},
      {path: 'character-creator/components/careers/noble/event', component: NobleEventComponent},
      {path: 'character-creator/components/careers/rogue/event', component: RogueEventComponent},
      {path: 'character-creator/components/careers/scholar/event', component: ScholarEventComponent},
      {path: 'character-creator/components/careers/scout/event', component: ScoutEventComponent},
      {path: 'character-creator/components/careers/prisoner/event', component: PrisonerEventComponent},
      {path: 'character-creator/components/careers/psion/event', component: PsionEventComponent},
      {path: 'character-creator/components/careers/mishap', component: CareerMishapComponent},
      {path: 'character-creator/components/careers/agent/mishap', component: AgentMishapComponent},
      {path: 'character-creator/components/careers/commission', component: CareerCommissionComponent},
      {path: 'character-creator/components/careers/advancement', component: CareerAdvancementComponent},
      {path: 'character-creator/components/careers/leaving', component: CareerLeavingComponent},
      {path: 'character-creator/components/careers/benefits', component: CareerMusterOutComponent},
      {path: 'character-creator/components/careers/draft', component: DraftComponent},
      {path: 'character-creator/final-steps', component: FinalStepsComponent},
      {path: 'sector-creator', component: SectorCreatorComponent},
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
