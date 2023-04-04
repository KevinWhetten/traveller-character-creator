export class Characteristic {
  max: number;
  current: number;
}

export class Characteristics {
  Strength: Characteristic = {max: 0, current: 0} as Characteristic;
  Dexterity: Characteristic = {max: 0, current: 0} as Characteristic;
  Endurance: Characteristic = {max: 0, current: 0} as Characteristic;
  Intellect: Characteristic = {max: 0, current: 0} as Characteristic;
  Education: Characteristic = {max: 0, current: 0} as Characteristic;
  SocialStanding: Characteristic = {max: 0, current: 0} as Characteristic;
  Psi: Characteristic = {max: 0, current: 0} as Characteristic;
}
