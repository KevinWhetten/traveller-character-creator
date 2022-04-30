import {Injectable} from '@angular/core';
import {Skill} from "../models/skill";

@Injectable({
  providedIn: 'root'
})
export class SkillService {
  private skillset: Skill[] = [
    {
      name: 'Admin',
      score: -3,
      description: 'This skill covers bureaucracies and administration of all sorts, including the navigation of bureaucratic obstacles or disasters. It also covers tracking inventories, ship manifests, and other records.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Advocate',
      score: -3,
      description: 'Advocate gives a knowledge of common legal codes and practises, especially interstellar law. It also gives the Traveller experience in oratory, debate, and public speaking, making it an excellent skill for lawyers and politicians.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Animals',
      score: -3,
      description: 'This skill, rare on industrialised or technologically advanced worlds, is for the care of animals.',
      subskills: [
        {
          name: 'Handling',
          score: -3,
          description: 'The Traveller knows how to handle an animal and ride those trained to bear a rider. Unusual animals raise the difficulty of the check.',
        } as Skill,
        {
          name: 'Training',
          score: -3,
          description: 'The Traveller knows how to tame and train animals.',
        } as Skill,
        {
          name: 'Veterinary',
          score: -3,
          description: 'The Traveller is trained in veterinary medicine and animal care.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Art',
      score: -3,
      description: 'The Traveller is trained in a type of creative art.',
      subskills: [
        {
          name: 'Performer',
          score: -3,
          description: 'The Traveller is a trained actor, dancer, or singer at home on the stage, screen, or holo.',
        } as Skill,
        {
          name: 'Holography',
          score: -3,
          description: 'Recording and producing aesthetically pleasing and clear holographic images.',
        } as Skill,
        {
          name: 'Instrument',
          score: -3,
          description: 'Playing a particular musical instrument, such as a flute, piano, or organ.',
        } as Skill,
        {
          name: 'Visual Media',
          score: -3,
          description: 'Making artistic or abstract paintings or sculptures in a variety of media.',
        } as Skill,
        {
          name: 'Write',
          score: -3,
          description: 'Composing inspiring or interesting pieces of text.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Astrogation',
      score: -3,
      description: 'This skill is for plotting the courses of starships and calculating accurate jumps.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Athletics',
      score: -3,
      description: 'The Traveller is a trained athlete and is physically fit. the Athletics skill effectively augments a Traveller\'s physical characteristics, whatever you can do with Strength alone you can also add your Athletics(Strength) DM to, for example. Athletics is also the principal skill used in adverse gravitational environments, specifically Athletics(Dexterity) in low or zero-G, and Athletics(Strength) in high-G locations.',
      subskills: [
        {
          name: 'Strength',
          score: -3,
          description: 'Feats of strength, weight-lifting, etc.',
        } as Skill,
        {
          name: 'Dexterity',
          score: -3,
          description: 'Climbing, juggling, throwing, etc. For alien races with wings, this also includes flying.',
        } as Skill,
        {
          name: 'Endurance',
          score: -3,
          description: 'Long-distance running, hiking, etc.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Broker',
      score: -3,
      description: 'The Broker skill allows a Traveller to negotiate trades and arrange fair deals. It is heavily used when trading.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Carouse',
      score: -3,
      description: 'Carousing is the art of socialising, having fun, but also ensuring other people have fun, and infectious good humour. It also covers social awareness and subterfuge in such situations.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Deception',
      score: -3,
      description: 'Deception allows a Traveller to lie fluently, disguise himself, perform sleight of hand, and fool onlookers. Most underhanded ways of cheating and lying fall under deception.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Diplomat',
      score: -3,
      description: 'The Diplomat skill is for negotiating deals, establishing peaceful contact, and smoothing over social faux pas. It includes how to behave in high society and proper ways to address nobles. It is a much more formal skill than persuade.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Drive',
      score: -3,
      description: 'This skill is for controlling ground vehicles of various types.',
      subskills: [
        {
          name: 'Hovercraft',
          score: -3,
          description: 'Vehicles that rely on a cushion of air and thrusters for motion.',
        } as Skill,
        {
          name: 'Mole',
          score: -3,
          description: 'For controlling vehicles that move through solid matter using drills or other earth-moving technologies, such as plasma torches or cavitation.',
        } as Skill,
        {
          name: 'Track',
          score: -3,
          description: 'For tanks and other vehicles that move on tracks.',
        } as Skill,
        {
          name: 'Walker',
          score: -3,
          description: 'Vehicles that use two or more legs to manoeuvre.',
        } as Skill,
        {
          name: 'Wheel',
          score: -3,
          description: 'For automobiles and similar ground cars.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Electronics',
      score: -3,
      description: 'This skill is used to operate electronic devices such as computers and ship-board systems. Higher levels represent the ability to repair and create electronic devices and systems.',
      subskills: [
        {
          name: 'Comms',
          score: -3,
          description: 'The use of modern telecommunications; opening communication channels, querying computer networks, jamming signals, and so on, as well as proper protocols for communicating with starports and other spacecraft.',
        } as Skill,
        {
          name: 'Computers',
          score: -3,
          description: 'Using and controlling computer systems adn similar electronics and electrics.',
        } as Skill,
        {
          name: 'Remote Ops',
          score: -3,
          description: 'Using telepresence to remotely control drones, missiles, robots, and other devices.',
        } as Skill,
        {
          name: 'Sensors',
          score: -3,
          description: 'The use and interpretation of data from electronic sensor devices, from observation satellites and remote probes to thermal imaging and densitometers.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Engineer',
      score: -3,
      description: 'The Engineer skill is used to operate and maintain spacecraft and advanced vehicles. Engineer can be used to make repairs on damaged systems on spacecraft and advanced vehicles. For repairs on simpler machines and systems, use the Mechanic skill.',
      subskills: [
        {
          name: 'M-Drive',
          score: -3,
          description: 'Maintaining and operating a spacecraft\'s manoeuvre drive, as well as its artificial gravity.',
        } as Skill,
        {
          name: 'J-Drive',
          score: -3,
          description: 'Maintaining and operating a spacecraft\'s Jump drive.',
        } as Skill,
        {
          name: 'Life Support',
          score: -3,
          description: 'Covers oxygen generators, heating, and lighting; and other necessary life support systems.',
        } as Skill,
        {
          name: 'Power',
          score: -3,
          description: 'Maintaining and operating a spacecraft\'s power plant.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Explosives',
      score: -3,
      description: 'The Explosives skill covers the use of demolition charges and other explosive devices, including assembling or disarming bombs. A failed Explosives check with an Effect of -4 or less can result in a bomb detonating prematurely.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Flyer',
      score: -3,
      description: 'The various specialities of this skill cover different types of flying vehicles. Flyers only work in an atmosphere, vehicles that can leave the atmosphere and enter orbit generally use the Pilot skill.',
      subskills: [
        {
          name: 'Airship',
          score: -3,
          description: 'Used for airships, dirigibles, and other powered lighter than air craft.',
        } as Skill,
        {
          name: 'Grav',
          score: -3,
          description: 'This covers air/rafts, grav belts, and other vehicles that use gravitic technology.',
        } as Skill,
        {
          name: 'Ornithopter',
          score: -3,
          description: 'For vehicles that fly through the use of flapping wings.',
        } as Skill,
        {
          name: 'Rotor',
          score: -3,
          description: 'For helicopters, tilt-rotors, and aerodynes.',
        } as Skill,
        {
          name: 'Wing',
          score: -3,
          description: 'For jets, vectored thrust aircraft, and aeroplanes using a lifting body.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Gambler',
      score: -3,
      description: 'The Traveller is familiar with a wide variety of gambling games, such as poker, roulette, blackjack, horse-racing, sports betting, and so on, and also has an excellent grasp of statistics and probability. Gambler increases the rewards from Benefit Rolls, giving the Traveller DM+1 to his cash rolls if he has Gambler 1 or better.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Gunner',
      score: -3,
      description: 'The various specialities of this skill deal with the operation of ship-mounted weapons in space combat. Most Travellers have smaller ships equipped solely with turret weapons.',
      subskills: [
        {
          name: 'Turret',
          score: -3,
          description: 'Operating turret-mounted weapons on board a ship.',
        } as Skill,
        {
          name: 'Ortillery',
          score: -3,
          description: 'A contraction of Orbital Artillery - using a ship\'s weapons for planetary bombardment or attacks on stationary targets.',
        } as Skill,
        {
          name: 'Screen',
          score: -3,
          description: 'Activating and using a ship\'s energy screens like Black Globe generators or meson screens.',
        } as Skill,
        {
          name: 'Capital',
          score: -3,
          description: 'Operating bay or spinal mount weapons on board a ship.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Gun Combat',
      score: -3,
      description: 'The Gun Combat skill covers a variety of ranged weapons.',
      subskills: [
        {
          name: 'Archaic',
          score: -3,
          description: 'For primitive weapons that are not thrown, such as bows and blowpipes.',
        } as Skill,
        {
          name: 'Energy',
          score: -3,
          description: 'Using advanced energy weapons like laser pistols or plasma rifles.',
        } as Skill,
        {
          name: 'Slug',
          score: -3,
          description: 'Weapons that fire a solid projectile such as the autorifle or gauss rifle.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Heavy Weapons',
      score: -3,
      description: 'The Heavy Weapons skill covers man-portable and larger weapons that cause extreme property damage, such as rocket launchers, artillery, and large plasma weapons.',
      subskills: [
        {
          name: 'Artillery',
          score: -3,
          description: 'Fixed guns, mortars, and other indirect-fire weapons.',
        } as Skill,
        {
          name: 'Man Portable',
          score: -3,
          description: 'Missile launchers, flamethrowers, and man-portable fusion and plasma.',
        } as Skill,
        {
          name: 'Vehicle',
          score: -3,
          description: 'Large weapons typically mounted on vehicles or strong-points such as tank guns and autocannon.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Investigate',
      score: -3,
      description: 'The Investigate skill incorporates keen observation, forensics, and detailed analysis.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Jack-of-All-Trades',
      score: -3,
      description: 'The Jack-of-All-Trades skill works differently to other skills. It reduces the unskilled penalty a Traveller receives for not having the appropriate skill by one for every level of Jack-of-All-Trades. With Jack-of-All-Trades 3, a Traveller can totally negate the penalty for being unskilled.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Language',
      score: -3,
      description: 'There are numerous different Language specialities, each one covering reading and writing a different language. All Travellers can speak and read their native language without needing the Language skill, and automated computer translator programs mean Language sills are not always needed on other worlds. Having Language 0 implies the Traveller has a smattering of simple phrases in several languages. There are, of course, as many specialities of Language as there are actual languages. Check with your GM to see what languages are present in your game world.',
      subskills: [
        {
          name: 'Language1',
          score: -3,
          description: 'The ability to read/write this language.',
        } as Skill,
        {
          name: 'Language2',
          score: -3,
          description: 'The ability to read/write this language.',
        } as Skill,
        {
          name: 'Language3',
          score: -3,
          description: 'The ability to read/write this language.',
        } as Skill,
        {
          name: 'Language4',
          score: -3,
          description: 'The ability to read/write this language.',
        } as Skill,
        {
          name: 'Language5',
          score: -3,
          description: 'The ability to read/write this language.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Leadership',
      score: -3,
      description: 'The Leadership skill is for directing, inspiring, and rallying allies and comrades. A Traveller may make a Leadership action in combat.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Mechanic',
      score: -3,
      description: 'The mechanic skill allows a Traveller to maintain and repair most equipment - some advanced equipment and spacecraft components require the Engineer skill. Unlike the narrower and more focussed Engineer or Science skills, Mechanic does not allow a Traveller to build new devices or alter existing ones - it is purely for repairs and maintenance, but covers all types of equipment.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Medic',
      score: -3,
      description: 'The Medic skill covers emergency first aid and battlefield triage, as well as diagnosis, treatment, surgery, and long-term care.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Melee',
      score: -3,
      description: 'The Melee skill covers attacking in hand-to-hand combat and the use of suitable weapons.',
      subskills: [
        {
          name: 'Unarmed',
          score: -3,
          description: 'Punching, kicking, wrestling, using improvised weapons in a bar brawl, etc. Also includes weapons that are part of an alien or creature, such as claws or teeth.',
        } as Skill,
        {
          name: 'Blade',
          score: -3,
          description: 'Attacking with swords, rapiers, blades, and other edged weapons.',
        } as Skill,
        {
          name: 'Bludgeon',
          score: -3,
          description: 'Attacking with maces, clubs, staves, etc.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Navigation',
      score: -3,
      description: 'Navigation is the planetside counterpart of astrogation, covering plotting courses and finding directions on the ground.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Persuade',
      score: -3,
      description: 'Persuade is a more casual, informal version of Diplomat. It covers fast-talking, bargaining, wheedling, and bluffing. It also covers bribery or intimidation.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Pilot',
      score: -3,
      description: 'The Pilot skill specialities cover different forms of spacecraft.',
      subskills: [
        {
          name: 'Small Craft',
          score: -3,
          description: 'Shuttles and other craft under 100 tons.',
        } as Skill,
        {
          name: 'Spacecraft',
          score: -3,
          description: 'Trade ships and other vessels between 100 and 5,000 tons.',
        } as Skill,
        {
          name: 'Capital Ships',
          score: -3,
          description: 'Battleships and other ships over 5,000 tons.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Profession',
      score: -3,
      description: 'A Traveller with a Profession skill is trained in producing useful goods or services. There are many different Profession specialities, but each one works the same way - the Traveller can make a Profession check to earn money on a planet that supports that trade. The amount of money raised is Cr250 * the Effect of the check per month. Unlike other skills with specialities, levels in the Profession skill do not grant the ability to use other specialities at level 0. Each speciality must be learned individually. Someone with a Profession skill of 0 has a general grasp of working for a living, but little experience beyond the most menial jobs.',
      subskills: [
        {
          name: 'Profession1',
          score: -3,
          description: 'The ability to make money from this profession.',
        } as Skill,
        {
          name: 'Profession2',
          score: -3,
          description: 'The ability to make money from this profession.',
        } as Skill,
        {
          name: 'Profession3',
          score: -3,
          description: 'The ability to make money from this profession.',
        } as Skill,
        {
          name: 'Profession4',
          score: -3,
          description: 'The ability to make money from this profession.',
        } as Skill,
        {
          name: 'Profession5',
          score: -3,
          description: 'The ability to make money from this profession.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Recon',
      score: -3,
      description: 'A Traveller trained in Recon is able to scout out dangers and spot threats, unusual objects, or out of place people.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Science',
      score: -3,
      description: 'The Science skill covers not just knowledge, but also practical application of that knowledge where such practical application is possible.',
      subskills: [
        {
          name: 'Archaeology',
          score: -3,
          description: 'The study of ancient civilizations. It also covers techniques of investigation and excavation.',
        } as Skill,
        {
          name: 'Astronomy',
          score: -3,
          description: 'The study of stars and celestial phenomena.',
        } as Skill,
        {
          name: 'Biology',
          score: -3,
          description: 'The study of living organisms.',
        } as Skill,
        {
          name: 'Chemistry',
          score: -3,
          description: 'The study of matter at the atomic, molecular, and macromolecular levels.',
        } as Skill,
        {
          name: 'Cosmology',
          score: -3,
          description: 'The study of the universe and its creation.',
        } as Skill,
        {
          name: 'Cybernetics',
          score: -3,
          description: 'The study of blending living and synthetic life.',
        } as Skill,
        {
          name: 'Economics',
          score: -3,
          description: 'The study of trade and markets.',
        } as Skill,
        {
          name: 'Genetics',
          score: -3,
          description: 'The study of genetic codes and engineering.',
        } as Skill,
        {
          name: 'History',
          score: -3,
          description: 'The study of the past, as seen through documents and records as opposed to physical artefacts.',
        } as Skill,
        {
          name: 'Linguistics',
          score: -3,
          description: 'The study of languages.',
        } as Skill,
        {
          name: 'Philosophy',
          score: -3,
          description: 'The study of beliefs and religions.',
        } as Skill,
        {
          name: 'Physics',
          score: -3,
          description: 'The study of fundamental forces.',
        } as Skill,
        {
          name: 'Planetology',
          score: -3,
          description: 'The study of planet formation and evolution.',
        } as Skill,
        {
          name: 'Psionicology',
          score: -3,
          description: 'The study of psionic powers and phenomena.',
        } as Skill,
        {
          name: 'Psychology',
          score: -3,
          description: 'The study of thought and society.',
        } as Skill,
        {
          name: 'Robotics',
          score: -3,
          description: 'The study of robot construction and use.',
        } as Skill,
        {
          name: 'Sophontology',
          score: -3,
          description: 'The study of intelligent living creatures.',
        } as Skill,
        {
          name: 'Xenology',
          score: -3,
          description: 'The study of alien life forms.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Seafarer',
      score: -3,
      description: 'The Seafarer skill covers all manner of watercraft and ocean travel.',
      subskills: [
        {
          name: 'Ocean Ships',
          score: -3,
          description: 'For motorised sea-going vessels.',
        } as Skill,
        {
          name: 'Personal',
          score: -3,
          description: 'Used for very small waterborne craft such as canoes and rowboats.',
        } as Skill,
        {
          name: 'Sail',
          score: -3,
          description: 'This skill is for wind-driven watercraft.',
        } as Skill,
        {
          name: 'Submarine',
          score: -3,
          description: 'For vehicles that travel underwater.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Stealth',
      score: -3,
      description: 'A Traveller trained in the Stealth skill is adept at staying unseen, unheard, and unnoticed.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Steward',
      score: -3,
      description: 'The Steward skill allows the Traveller to serve and care for nobles and high-class passengers. It covers everything from proper address and behaviour, to cooking and tailoring, as well as basic management skills. A Traveller with the Steward skill is necessary on any ship offering High Passage.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Streetwise',
      score: -3,
      description: 'A Traveller with the Streetwise skill understands the urban environment and the power structures in society. On his homeworld and in related systems, he knows criminal contacts and fixers. On other worlds, he can quickly intuit power structures and can fit into the local underworld.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Survival',
      score: -3,
      description: 'The Survival skill is the wilderness counterpart of the urban Streetwise skill - The Traveller is trained to survive in the wild, build shelters, hunt or trap animals, avoid exposure, and so forth. He can recognize plants and animals of his homeworld and related planets, and can pick up on common clues and traits even on unfamiliar worlds.',
      subskills: [] as Skill[]
    } as Skill,
    {
      name: 'Tactics',
      score: -3,
      description: 'This skill covers planning and decision making, from board games to squad level combat to fleet engagements.',
      subskills: [
        {
          name: 'Military',
          score: -3,
          description: 'Coordinating the attacks of foot troops or vehicles on the ground.',
        } as Skill,
        {
          name: 'Naval',
          score: -3,
          description: 'Coordinating the attacks of a spacecraft or fleet.',
        } as Skill
      ]
    } as Skill,
    {
      name: 'Vacc Suit',
      score: -3,
      description: 'The Vacc Suit skill allows a Traveller to wear and operate spacesuits and environmental suits. A Traveller will rarely need to make Vacc Suit checks under ordinary circumstances - merely possessing the skill is enough. If the Traveller does not have the requisite Vacc Suit skill for the suit he is wearing, he suffers DM-2 to all skill checks made while wearing a suit for each missing level. This skill also permits the character to operate advanced battle armour.',
      subskills: [] as Skill[]
    } as Skill,
  ];

  constructor() {
  }

  getSkillScore(skillName: string) {
    let skill = this.skillset.find(x => x.name == skillName) as Skill;
    return skill.score;
  }

  getSkill(skillName: string) {
    return this.skillset.find(x => x.name == skillName) as Skill;
  }

  updateSkills(skillUpdates: { skillName: string; value: number }[]) {
    for (let skillUpdate of skillUpdates) {
      this.updateSkill(skillUpdate.skillName, skillUpdate.value);
    }
  }

  updateSkill(skillName: string, value: number): boolean {
    let skill = this.skillset.find(x => x.name == skillName) as Skill;
    if(skill.score < value){
      skill.score = value;
      return true;
    }
    return false;
  }

  getSkillset() {
    return this.skillset;
  }

  newTerm() {
    for (let baseSkill of this.skillset) {
      baseSkill.learnedThisTerm = false;
      for (let subskill of baseSkill.subskills) {
        subskill.learnedThisTerm = false;
      }
    }
  }
}
