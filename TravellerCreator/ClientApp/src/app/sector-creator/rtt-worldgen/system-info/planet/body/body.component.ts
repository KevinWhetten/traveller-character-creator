import {Component, Input, OnInit} from '@angular/core';
import {Planet} from "../../../../models/sector-creator/hex/system/planet/planet";

interface Desired {
  Name: string;
  Desired: number;
}

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.scss']
})
export class BodyComponent implements OnInit {
  @Input() body: Planet;

  constructor() {
  }

  ngOnInit(): void {
  }

  DecToHex(number: number): string {
    if (number <= 9) {
      return number.toString();
    } else {
      switch (number) {
        case 10:
          return "A";
        case 11:
          return "B";
        case 12:
          return "C";
        case 13:
          return "D";
        case 14:
          return "E";
        case 15:
          return "F";
      }
    }
    return "";
  }

  getCode(body: Planet) {
    let name = ""; //body.name;
    let starport = body.settlement.starport;
    let size = this.DecToHex(body.size);
    let atmosphere = this.DecToHex(body.atmosphere);
    let hydrographics = this.DecToHex(body.hydrosphere);
    let population = this.DecToHex(body.settlement.population);
    let government = this.DecToHex(body.settlement.government);
    let lawLevel = this.DecToHex(body.settlement.lawLevel);
    let techLevel = this.DecToHex(body.settlement.techLevel);
    let remarks = body.tradeCodes.join(" ");
    let importance = `{ ${body.importance} }`;
    let economics = `(${this.DecToHex(body.settlement.economics.resources)}${this.DecToHex(body.settlement.economics.labor)}${this.DecToHex(body.settlement.economics.infrastructure)}${body.settlement.economics.efficiency >= 0 ? `+${body.settlement.economics.efficiency}` : body.settlement.economics.efficiency})`;
    let culture = `[${this.DecToHex(body.settlement.culture.homogeneity)}${this.DecToHex(body.settlement.culture.acceptance)}${this.DecToHex(body.settlement.culture.strangeness)}${this.DecToHex(body.settlement.culture.symbols)}]`;

    return `${name}  ${starport}${size}${atmosphere}${hydrographics}${population}${government}${lawLevel}-${techLevel}  ${remarks}  ${importance}  ${economics}  ${culture}`;
  }

  GetMostDesiringRace() {
    let desired = [] as Desired[];

    let mostDesiredNumber = Math.max(this.body.desirability.Human, this.body.desirability.Aslan, this.body.desirability.Mannu, this.body.desirability.Largosian, this.body.desirability.Tortosian, this.body.desirability.Ithromir, this.body.desirability.Chrotos, this.body.desirability.KaSara, this.body.desirability.TheCollective, this.body.desirability.Vargr, this.body.desirability.Scrapper);

    if (this.body.desirability.Human == mostDesiredNumber) {
      desired.push({Name: "Human", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Aslan == mostDesiredNumber) {
      desired.push({Name: "Aslan", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Mannu == mostDesiredNumber) {
      desired.push({Name: "Mannu", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Largosian == mostDesiredNumber) {
      desired.push({Name: "Largosian", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Tortosian == mostDesiredNumber) {
      desired.push({Name: "Tortosian", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Ithromir == mostDesiredNumber) {
      desired.push({Name: "Ithromir", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Chrotos == mostDesiredNumber) {
      desired.push({Name: "Chrotos", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.KaSara == mostDesiredNumber) {
      desired.push({Name: "Ka'Sara", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.TheCollective == mostDesiredNumber) {
      desired.push({Name: "The Collective", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Vargr == mostDesiredNumber) {
      desired.push({Name: "Vargr", Desired: mostDesiredNumber} as Desired)
    }
    if (this.body.desirability.Scrapper == mostDesiredNumber) {
      desired.push({Name: "Scrapper", Desired: mostDesiredNumber} as Desired)
    }

    return desired;
  }

}
