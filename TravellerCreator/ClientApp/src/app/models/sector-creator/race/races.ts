import {Race} from "./race";

export class Races {
  static Human: Race = {
    name: "humans",
    allegianceCode: "Hu",
    techLevel: 11,
    expansionLevel: 7,
    homeworldCoordinates: {column: 23, row: 11}
  };
  static Aslan: Race = {
    name: "aslan",
    allegianceCode: "As",
    techLevel: 10,
    expansionLevel: 6,
    homeworldCoordinates: {column: 17, row: 23}
  };
  static Mannu: Race = {
    name: "mannu",
    allegianceCode: "Ma",
    techLevel: 10,
    expansionLevel: 6,
    homeworldCoordinates: {column: 13, row: 8}
  };
  static Largosians: Race = {
    name: "largosians",
    allegianceCode: "La",
    techLevel: 10,
    expansionLevel: 7,
    homeworldCoordinates: {column: 17, row: 32}
  };
  static Tortosians: Race = {
    name: "tortosians",
    allegianceCode: "To",
    techLevel: 9,
    expansionLevel: 6,
    homeworldCoordinates: {column: 27, row: 31}
  };
  static Ithromir: Race = {
    name: "ithromir",
    allegianceCode: "It",
    techLevel: 13,
    expansionLevel: 8,
    homeworldCoordinates: {column: 8, row: 27}
  };
  static Chrotos: Race = {
    name: "chrotos",
    allegianceCode: "Ch",
    techLevel: 12,
    expansionLevel: 6,
    homeworldCoordinates: {column: 15, row: 17}
  };
  static TheCollective: Race = {
    name: "theCollective",
    allegianceCode: "Co",
    techLevel: 13,
    expansionLevel: 4,
    homeworldCoordinates: {column: 29, row: 18}
  };
  static KaSara: Race = {
    name: "kaSara",
    allegianceCode: "Ka",
    techLevel: 12,
    expansionLevel: 7,
    homeworldCoordinates: {column: 27, row: 2}
  };
  static Scrappers: Race = {
    name: "scrappers",
    allegianceCode: "Sc",
    techLevel: 15,
    expansionLevel: 2,
    homeworldCoordinates: {column: 5, row: 16}
  };
  static Vargr: Race = {
    name: "vargr",
    allegianceCode: "Vr",
    techLevel: 11,
    expansionLevel: 6,
    homeworldCoordinates: {column: 26, row: 24}
  } as Race;

  static AllRaces: Race[] = [this.Human, this.Aslan, this.Mannu, this.Largosians, this.Tortosians, this.Ithromir, this.Chrotos, this.TheCollective, this.KaSara, this.Scrappers, this.Vargr];
}
