import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Group} from "../../models/group";
import {SkillService} from "../../services/skill.service";

@Component({
  selector: 'app-skill-select',
  templateUrl: './skill-select.component.html',
  styleUrls: ['./skill-select.component.scss']
})
export class SkillSelectComponent implements OnInit {
  @Input() groups: Group[] = [];
  @Input() label: string = 'Choose one';
  @Input() type: string = 'full';
  @Input() targetSkillLevel: number = 0;
  @Output() onChange = new EventEmitter<string>();
  selectedSkill: string;
  private universitySkills: string[] = ['Admin', 'Advocate', 'Animals', 'Training', 'Veterinary', 'Art', 'Performance',
    'Holography', 'Instrument', 'VisualMedia', 'Write', 'Astrogation', 'Electronics', 'Comms', 'Computers', 'RemoteOps',
    'Sensors', 'Engineer', 'MDrive', 'JDrive', 'LifeSupport', 'Power', 'Language', 'Language1', 'Language2',
    'Language3', 'Language4', 'Language5', 'Medic', 'Navigation', 'Profession', 'Profession1', 'Profession2',
    'Profession3', 'Profession4', 'Profession5', 'Science', 'Archaeology', 'Astronomy', 'Biology', 'Chemistry',
    'Cosmology', 'Cybernetics', 'Economics', 'Genetics', 'History', 'Linguistics', 'Philosophy', 'Physics',
    'Planetology', 'Psionicology', 'Psychology', 'Robotics', 'Sophontology', 'Xenology'];

  constructor(private _skillService: SkillService) {
  }

  ngOnInit(): void {
    if (this.groups.length == 0) {
      if (this.type == 'basic') {
        this.groups = [
          {
            name: 'Basic Skills',
            skills: []
          }
        ];
        this.populateBasicSkills();
      }
      if (this.type == 'basic-university') {
        this.groups = [
          {
            name: 'Basic Skills',
            skills: []
          }
        ];
        this.populateBasicUniversitySkills();
      }
      if (this.type == 'full') {
        this.groups = [];
        this.populateFullSkills();
      }
      if (this.type == 'full-university') {
        this.groups = [];
        this.populateFullUniversitySkills();
      }
      if (this.type == 'this-term') {
        this.populateThisTermSkills();
      }
    }
  }

  change() {
    this.onChange.emit(this.selectedSkill);
  }

  private populateFullSkills() {
    let characterSkills = this._skillService.getSkillset();

    for (let baseSkill of characterSkills) {
      if (baseSkill.subskills.length == 0) {
        if (baseSkill.score == undefined || baseSkill.score < this.targetSkillLevel) {
          this.groups.push({name: baseSkill.name, skills: [baseSkill]} as Group);
        }
      } else {
        let group = {name: baseSkill.name, skills: []} as Group;
        for (let subskill of baseSkill.subskills) {
          if (subskill.score == undefined || subskill.score < this.targetSkillLevel) {
            group.skills.push(subskill);
          }
        }
      }
    }
  }

  private populateBasicSkills() {
    let characterSkills = this._skillService.getSkillset();

    for (let skill of characterSkills) {
      if (skill.score < 0) {
        this.groups[0].skills.push(skill);
      }
    }
  }

  private populateFullUniversitySkills() {
    let characterSkills = this._skillService.getSkillset();

    for (let baseSkill of characterSkills) {
      if (baseSkill.subskills == undefined) {
        if (this.universitySkills.indexOf(baseSkill.name) != -1 && (baseSkill.score == undefined || baseSkill.score < this.targetSkillLevel)) {
          this.groups.push({name: baseSkill.name, skills: [baseSkill]} as Group);
        }
      } else {
        let group = {name: baseSkill.name, skills: []} as Group;
        for (let subskill of baseSkill.subskills) {
          if (this.universitySkills.indexOf(subskill.name) != -1 && (subskill.score == undefined || subskill.score < this.targetSkillLevel)) {
            group.skills.push(subskill);
          }
        }
        if (group.skills.length > 0) {
          this.groups.push(group);
        }
      }
    }
  }

  private populateBasicUniversitySkills() {
    let characterSkills = this._skillService.getSkillset();

    for (let skill of characterSkills) {
      if (skill.score < 0 && this.universitySkills.indexOf(skill.name) != -1) {
        this.groups[0].skills.push(skill);
      }
    }
  }

  private populateThisTermSkills() {
    let characterSkills = this._skillService.getSkillset();

    for (let baseSkill of characterSkills) {
      if (baseSkill.subskills.length <= 0) {
        if (baseSkill.learnedThisTerm) {
          this.groups.push({name: baseSkill.name, skills: [baseSkill]} as Group);
        }
      } else {
        let group = {name: baseSkill.name, skills: []} as Group;
        for (let subskill of baseSkill.subskills) {
          if (baseSkill.learnedThisTerm || subskill.learnedThisTerm) {
            group.skills.push(subskill);
          }
        }
        if (group.skills.length > 0) {
          this.groups.push(group);
        }
      }
    }
  }
}
