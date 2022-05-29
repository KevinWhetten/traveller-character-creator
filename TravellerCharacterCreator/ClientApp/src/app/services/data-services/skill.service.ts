import {Injectable} from '@angular/core';
import {BaseSkill, Skill, Subskill} from 'src/app/models/skill';


@Injectable({
  providedIn: 'root'
})
export class SkillService {
  SkillName = {
    Admin: 'Admin',
    Advocate: 'Advocate',
    Animals: 'Animals',
    AnimalsHandling: 'Handling',
    AnimalsTraining: 'Training',
    AnimalsVeterinary: 'Veterinary',
    Art: 'Art',
    ArtPerformer: 'Performer',
    ArtHolography: 'Holography',
    ArtInstrument: 'Instrument',
    ArtVisualMedia: 'Visual Media',
    ArtWrite: 'Write',
    Astrogation: 'Astrogation',
    Athletics: 'Athletics',
    AthleticsStrength: 'Strength',
    AthleticsDexterity: 'Dexterity',
    AthleticsEndurance: 'Endurance',
    Broker: 'Broker',
    Carouse: 'Carouse',
    Deception: 'Deception',
    Diplomat: 'Diplomat',
    Drive: 'Drive',
    DriveHovercraft: 'Hovercraft',
    DriveMole: 'Mole',
    DriveTrack: 'Track',
    DriveWalker: 'Walker',
    DriveWheel: 'Wheel',
    Electronics: 'Electronics',
    ElectronicsComms: 'Comms',
    ElectronicsComputers: 'Computers',
    ElectronicsRemoteOps: 'Remote Ops',
    ElectronicsSensors: 'Sensors',
    Engineer: 'Engineer',
    EngineerMDrive: 'M-Drive',
    EngineerJDrive: 'J-Drive',
    EngineerLifeSupport: 'Life Support',
    EngineerPower: 'Power',
    Explosives: 'Explosives',
    Flyer: 'Flyer',
    FlyerAirship: 'Airship',
    FlyerGrav: 'Grav',
    FlyerOrnithopter: 'Ornithopter',
    FlyerRotor: 'Rotor',
    FlyerWing: 'Wing',
    Gambler: 'Gambler',
    Gunner: 'Gunner',
    GunnerTurret: 'Turret',
    GunnerOrtillery: 'Ortillery',
    GunnerScreen: 'Screen',
    GunnerCapital: 'Capital',
    GunCombat: 'Gun Combat',
    GunCombatArchaic: 'Archaic',
    GunCombatEnergy: 'Energy',
    GunCombatSlug: 'Slug',
    HeavyWeapons: 'Heavy Weapons',
    HeavyWeaponsArtillery: 'Artillery',
    HeavyWeaponsManPortable: 'Man Portable',
    HeavyWeaponsVehicle: 'Vehicle',
    Investigate: 'Investigate',
    JackOfAllTrades: 'Jack-of-All-Trades',
    Language: 'Language',
    Language1: 'Language1',
    Language2: 'Language2',
    Language3: 'Language3',
    Language4: 'Language4',
    Language5: 'Language5',
    Leadership: 'Leadership',
    Mechanic: 'Mechanic',
    Medic: 'Medic',
    Melee: 'Melee',
    MeleeUnarmed: 'Unarmed',
    MeleeBlade: 'Blade',
    MeleeBludgeon: 'Bludgeon',
    Navigation: 'Navigation',
    Persuade: 'Persuade',
    Pilot: 'Pilot',
    PilotSmallCraft: 'Small Craft',
    PilotSpacecraft: 'Spacecraft',
    PilotCapitalShips: 'Capital Ships',
    Profession: 'Profession',
    Profession1: 'Profession1',
    Profession2: 'Profession2',
    Profession3: 'Profession3',
    Profession4: 'Profession4',
    Profession5: 'Profession5',
    Recon: 'Recon',
    Science: 'Science',
    ScienceArchaeology: 'Archaeology',
    ScienceAstronomy: 'Astronomy',
    ScienceBiology: 'Biology',
    ScienceChemistry: 'Chemistry',
    ScienceCosmology: 'Cosmology',
    ScienceCybernetics: 'Cybernetics',
    ScienceEconomics: 'Economics',
    ScienceGenetics: 'Genetics',
    ScienceHistory: 'History',
    ScienceLinguistics: 'Linguistics',
    SciencePhilosophy: 'Philosophy',
    SciencePhysics: 'Physics',
    SciencePlanetology: 'Planetology',
    SciencePsionicology: 'Psionicology',
    SciencePsychology: 'Psychology',
    ScienceRobotics: 'Robotics',
    ScienceSophontology: 'Sophontology',
    ScienceXenology: 'Xenology',
    Seafarer: 'Seafarer',
    SeafarerOceanShips: 'Ocean Ships',
    SeafarerPersonal: 'Personal',
    SeafarerSail: 'Sail',
    SeafarerSubmarine: 'Submarine',
    Stealth: 'Stealth',
    Steward: 'Steward',
    Streetwise: 'Streetwise',
    Survival: 'Survival',
    Tactics: 'Tactics',
    TacticsMilitary: 'Military',
    TacticsNaval: 'Naval',
    VaccSuit: 'Vacc Suit'
  };
  skills: Skill[] = [
    {
      Name: this.SkillName.Admin,
      Description: 'This skill covers bureaucracies and administration of all sorts, including the navigation of bureaucratic obstacles or disasters. It also covers tracking inventories, ship manifests, and other records.',
    },
    {
      Name: this.SkillName.Advocate,
      Description: 'Advocate gives a knowledge of common legal codes and practises, especially interstellar law. It also gives the Traveller experience in oratory, debate, and public speaking, making it an excellent skill for lawyers and politicians.',
    },
    {
      Name: this.SkillName.Animals,
      Description: 'This skill, rare on industrialised or technologically advanced worlds, is for the care of animals.',
      Subskills: [this.SkillName.AnimalsHandling, this.SkillName.AnimalsTraining, this.SkillName.AnimalsVeterinary]
    } as BaseSkill,
    {
      Name: this.SkillName.AnimalsHandling,
      Description: 'The Traveller knows how to handle an animal and ride those trained to bear a rider. Unusual animals raise the difficulty of the check.',
      ParentSkill: this.SkillName.Animals
    } as Subskill,
    {
      Name: this.SkillName.AnimalsTraining,
      Description: 'The Traveller knows how to tame and train animals.',
      ParentSkill: this.SkillName.Animals
    } as Subskill,
    {
      Name: this.SkillName.AnimalsVeterinary,
      Description: 'The Traveller is trained in veterinary medicine and animal care.',
      ParentSkill: this.SkillName.Animals
    } as Subskill,
    {
      Name: this.SkillName.Art,
      Description: 'The Traveller is trained in a type of creative art.',
      Subskills: [this.SkillName.ArtPerformer, this.SkillName.ArtHolography, this.SkillName.ArtInstrument, this.SkillName.ArtVisualMedia, this.SkillName.ArtWrite]
    } as BaseSkill,
    {
      Name: this.SkillName.ArtPerformer,
      ParentSkill: this.SkillName.Art,
      Description: 'The Traveller is a trained actor, dancer, or singer at home on the stage, screen, or holo.',
    } as Subskill,
    {
      Name: this.SkillName.ArtHolography,
      ParentSkill: this.SkillName.Art,
      Description: 'Recording and producing aesthetically pleasing and clear holographic images.',
    } as Subskill,
    {
      Name: this.SkillName.ArtInstrument,
      ParentSkill: this.SkillName.Art,
      Description: 'Playing a particular musical instrument, such as a flute, piano, or organ.',
    } as Subskill,
    {
      Name: this.SkillName.ArtVisualMedia,
      ParentSkill: this.SkillName.Art,
      Description: 'Making artistic or abstract paintings or sculptures in a variety of media.',
    } as Subskill,
    {
      Name: this.SkillName.ArtWrite,
      ParentSkill: this.SkillName.Art,
      Description: 'Composing inspiring or interesting pieces of text.',
    } as Subskill,
    {
      Name: this.SkillName.Astrogation,
      Description: 'This skill is for plotting the courses of starships and calculating accurate jumps.'
    },
    {
      Name: this.SkillName.Athletics,
      Description: 'The Traveller is a trained athlete and is physically fit. the Athletics skill effectively augments a Traveller\'s physical characteristics, whatever you can do with Strength alone you can also add your Athletics(Strength) DM to, for example. Athletics is also the principal skill used in adverse gravitational environments, specifically Athletics(Dexterity) in low or zero-G, and Athletics(Strength) in high-G locations.',
      Subskills: [this.SkillName.AthleticsStrength, this.SkillName.AthleticsDexterity, this.SkillName.AthleticsEndurance]
    } as BaseSkill,
    {
      Name: this.SkillName.AthleticsStrength,
      ParentSkill: this.SkillName.Athletics,
      Description: 'Feats of strength, weight-lifting, etc.',
    } as Subskill,
    {
      Name: this.SkillName.AthleticsDexterity,
      ParentSkill: this.SkillName.Athletics,
      Description: 'Climbing, juggling, throwing, etc. For alien races with wings, this also includes flying.',
    } as Subskill,
    {
      Name: this.SkillName.AthleticsEndurance,
      ParentSkill: this.SkillName.Athletics,
      Description: 'Long-distance running, hiking, etc.',
    } as Subskill,
    {
      Name: this.SkillName.Broker,
      Description: 'The Broker skill allows a Traveller to negotiate trades and arrange fair deals. It is heavily used when trading.'
    },
    {
      Name: this.SkillName.Carouse,
      Description: 'Carousing is the art of socialising, having fun, but also ensuring other people have fun, and infectious good humour. It also covers social awareness and subterfuge in such situations.'
    },
    {
      Name: this.SkillName.Deception,
      Description: 'Deception allows a Traveller to lie fluently, disguise himself, perform sleight of hand, and fool onlookers. Most underhanded ways of cheating and lying fall under deception.'
    },
    {
      Name: this.SkillName.Diplomat,
      Description: 'The Diplomat skill is for negotiating deals, establishing peaceful contact, and smoothing over social faux pas. It includes how to behave in high society and proper ways to address nobles. It is a much more formal skill than persuade.'
    },
    {
      Name: this.SkillName.Drive,
      Description: 'This skill is for controlling ground vehicles of various types.',
      Subskills: [this.SkillName.DriveHovercraft, this.SkillName.DriveMole, this.SkillName.DriveTrack, this.SkillName.DriveWalker, this.SkillName.DriveWheel]
    } as BaseSkill,
    {
      Name: this.SkillName.DriveHovercraft,
      ParentSkill: this.SkillName.Drive,
      Description: 'Vehicles that rely on a cushion of air and thrusters for motion.',
    } as Subskill,
    {
      Name: this.SkillName.DriveMole,
      ParentSkill: this.SkillName.Drive,
      Description: 'For controlling vehicles that move through solid matter using drills or other earth-moving technologies, such as plasma torches or cavitation.',
    } as Subskill,
    {
      Name: this.SkillName.DriveTrack,
      ParentSkill: this.SkillName.Drive,
      Description: 'For tanks and other vehicles that move on tracks.',
    } as Subskill,
    {
      Name: this.SkillName.DriveWalker,
      ParentSkill: this.SkillName.Drive,
      Description: 'Vehicles that use two or more legs to manoeuvre.',
    } as Subskill,
    {
      Name: this.SkillName.DriveWheel,
      ParentSkill: this.SkillName.Drive,
      Description: 'For automobiles and similar ground cars.',
    } as Subskill,
    {
      Name: this.SkillName.Electronics,
      Description: 'This skill is used to operate electronic devices such as computers and ship-board systems. Higher levels represent the ability to repair and create electronic devices and systems.',
      Subskills: [this.SkillName.ElectronicsComms, this.SkillName.ElectronicsComputers, this.SkillName.ElectronicsRemoteOps, this.SkillName.ElectronicsSensors]
    } as BaseSkill,
    {
      Name: this.SkillName.ElectronicsComms,
      ParentSkill: this.SkillName.Electronics,
      Description: 'The use of modern telecommunications; opening communication channels, querying computer networks, jamming signals, and so on, as well as proper protocols for communicating with starports and other spacecraft.',
    } as Subskill,
    {
      Name: this.SkillName.ElectronicsComputers,
      ParentSkill: this.SkillName.Electronics,
      Description: 'Using and controlling computer systems adn similar electronics and electrics.',
    } as Subskill,
    {
      Name: this.SkillName.ElectronicsRemoteOps,
      ParentSkill: this.SkillName.Electronics,
      Description: 'Using telepresence to remotely control drones, missiles, robots, and other devices.',
    } as Subskill,
    {
      Name: this.SkillName.ElectronicsSensors,
      ParentSkill: this.SkillName.Electronics,
      Description: 'The use and interpretation of data from electronic sensor devices, from observation satellites and remote probes to thermal imaging and densitometers.',
    } as Subskill,
    {
      Name: this.SkillName.Engineer,
      Description: 'The Engineer skill is used to operate and maintain spacecraft and advanced vehicles. Engineer can be used to make repairs on damaged systems on spacecraft and advanced vehicles. For repairs on simpler machines and systems, use the Mechanic skill.',
      Subskills: [this.SkillName.EngineerMDrive, this.SkillName.EngineerJDrive, this.SkillName.EngineerLifeSupport, this.SkillName.EngineerPower]
    } as BaseSkill,
    {
      Name: this.SkillName.EngineerMDrive,
      ParentSkill: this.SkillName.Engineer,
      Description: 'Maintaining and operating a spacecraft\'s manoeuvre drive, as well as its artificial gravity.',
    } as Subskill,
    {
      Name: this.SkillName.EngineerJDrive,
      ParentSkill: this.SkillName.Engineer,
      Description: 'Maintaining and operating a spacecraft\'s Jump drive.',
    } as Subskill,
    {
      Name: this.SkillName.EngineerLifeSupport,
      ParentSkill: this.SkillName.Engineer,
      Description: 'Covers oxygen generators, heating, and lighting; and other necessary life support systems.',
    } as Subskill,
    {
      Name: this.SkillName.EngineerPower,
      ParentSkill: this.SkillName.Engineer,
      Description: 'Maintaining and operating a spacecraft\'s power plant.',
    } as Subskill,
    {
      Name: this.SkillName.Explosives,
      Description: 'The Explosives skill covers the use of demolition charges and other explosive devices, including assembling or disarming bombs. A failed Explosives check with an Effect of -4 or less can result in a bomb detonating prematurely.'
    },
    {
      Name: this.SkillName.Flyer,
      Description: 'The various specialities of this skill cover different types of flying vehicles. Flyers only work in an atmosphere, vehicles that can leave the atmosphere and enter orbit generally use the Pilot skill.',
      Subskills: [this.SkillName.FlyerAirship, this.SkillName.FlyerGrav, this.SkillName.FlyerOrnithopter, this.SkillName.FlyerRotor, this.SkillName.FlyerWing]
    } as BaseSkill,
    {
      Name: this.SkillName.FlyerAirship,
      ParentSkill: this.SkillName.Flyer,
      Description: 'Used for airships, dirigibles, and other powered lighter than air craft.',
    } as Subskill,
    {
      Name: this.SkillName.FlyerGrav,
      ParentSkill: this.SkillName.Flyer,
      Description: 'This covers air/rafts, grav belts, and other vehicles that use gravitic technology.',
    } as Subskill,
    {
      Name: this.SkillName.FlyerOrnithopter,
      ParentSkill: this.SkillName.Flyer,
      Description: 'For vehicles that fly through the use of flapping wings.',
    } as Subskill,
    {
      Name: this.SkillName.FlyerRotor,
      ParentSkill: this.SkillName.Flyer,
      Description: 'For helicopters, tilt-rotors, and aerodynes.',
    } as Subskill,
    {
      Name: this.SkillName.FlyerWing,
      ParentSkill: this.SkillName.Flyer,
      Description: 'For jets, vectored thrust aircraft, and aeroplanes using a lifting body.',
    } as Subskill,
    {
      Name: this.SkillName.Gambler,
      Description: 'The Traveller is familiar with a wide variety of gambling games, such as poker, roulette, blackjack, horse-racing, sports betting, and so on, and also has an excellent grasp of statistics and probability. Gambler increases the rewards from Benefit Rolls, giving the Traveller DM+1 to his cash rolls if he has Gambler 1 or better.'
    },
    {
      Name: this.SkillName.Gunner,
      Description: 'The various specialities of this skill deal with the operation of ship-mounted weapons in space combat. Most Travellers have smaller ships equipped solely with turret weapons.',
      Subskills: [this.SkillName.GunnerTurret, this.SkillName.GunnerOrtillery, this.SkillName.GunnerScreen, this.SkillName.GunnerCapital]
    } as BaseSkill,
    {
      Name: this.SkillName.GunnerTurret,
      ParentSkill: this.SkillName.Gunner,
      Description: 'Operating turret-mounted weapons on board a ship.',
    } as Subskill,
    {
      Name: this.SkillName.GunnerOrtillery,
      ParentSkill: this.SkillName.Gunner,
      Description: 'A contraction of Orbital Artillery - using a ship\'s weapons for planetary bombardment or attacks on stationary targets.',
    } as Subskill,
    {
      Name: this.SkillName.GunnerScreen,
      ParentSkill: this.SkillName.Gunner,
      Description: 'Activating and using a ship\'s energy screens like Black Globe generators or meson screens.',
    } as Subskill,
    {
      Name: this.SkillName.GunnerCapital,
      ParentSkill: this.SkillName.Gunner,
      Description: 'Operating bay or spinal mount weapons on board a ship.',
    } as Subskill,
    {
      Name: this.SkillName.GunCombat,
      Description: 'The Gun Combat skill covers a variety of ranged weapons.',
      Subskills: [this.SkillName.GunCombatArchaic, this.SkillName.GunCombatEnergy, this.SkillName.GunCombatSlug]
    } as BaseSkill,
    {
      Name: this.SkillName.GunCombatArchaic,
      ParentSkill: this.SkillName.GunCombat,
      Description: 'For primitive weapons that are not thrown, such as bows and blowpipes.',
    } as Subskill,
    {
      Name: this.SkillName.GunCombatEnergy,
      ParentSkill: this.SkillName.GunCombat,
      Description: 'Using advanced energy weapons like laser pistols or plasma rifles.',
    } as Subskill,
    {
      Name: this.SkillName.GunCombatSlug,
      ParentSkill: this.SkillName.GunCombat,
      Description: 'Weapons that fire a solid projectile such as the autorifle or gauss rifle.',
    } as Subskill,
    {
      Name: this.SkillName.HeavyWeapons,
      Description: 'The Heavy Weapons skill covers man-portable and larger weapons that cause extreme property damage, such as rocket launchers, artillery, and large plasma weapons.',
      Subskills: [this.SkillName.HeavyWeaponsArtillery, this.SkillName.HeavyWeaponsManPortable, this.SkillName.HeavyWeaponsVehicle]
    } as BaseSkill,
    {
      Name: this.SkillName.HeavyWeaponsArtillery,
      ParentSkill: this.SkillName.HeavyWeapons,
      Description: 'Fixed guns, mortars, and other indirect-fire weapons.',
    } as Subskill,
    {
      Name: this.SkillName.HeavyWeaponsManPortable,
      ParentSkill: this.SkillName.HeavyWeapons,
      Description: 'Missile launchers, flamethrowers, and man-portable fusion and plasma.',
    } as Subskill,
    {
      Name: this.SkillName.HeavyWeaponsVehicle,
      ParentSkill: this.SkillName.HeavyWeapons,
      Description: 'Large weapons typically mounted on vehicles or strong-points such as tank guns and autocannon.',
    } as Subskill,
    {
      Name: this.SkillName.Investigate,
      Description: 'The Investigate skill incorporates keen observation, forensics, and detailed analysis.'
    },
    {
      Name: this.SkillName.JackOfAllTrades,
      Description: 'The Jack-of-All-Trades skill works differently to other skills. It reduces the unskilled penalty a Traveller receives for not having the appropriate skill by one for every level of Jack-of-All-Trades. With Jack-of-All-Trades 3, a Traveller can totally negate the penalty for being unskilled.'
    },
    {
      Name: this.SkillName.Language,
      Description: 'There are numerous different Language specialities, each one covering reading and writing a different language. All Travellers can speak and read their native language without needing the Language skill, and automated computer translator programs mean Language sills are not always needed on other worlds. Having Language 0 implies the Traveller has a smattering of simple phrases in several languages. There are, of course, as many specialities of Language as there are actual languages. Check with your GM to see what languages are present in your game world.',
      Subskills: [this.SkillName.Language1, this.SkillName.Language2, this.SkillName.Language3, this.SkillName.Language4, this.SkillName.Language5]
    } as BaseSkill,
    {
      Name: this.SkillName.Language1,
      ParentSkill: this.SkillName.Language,
      Description: 'The ability to read/write this language.',
    } as Subskill,
    {
      Name: this.SkillName.Language2,
      ParentSkill: this.SkillName.Language,
      Description: 'The ability to read/write this language.',
    } as Subskill,
    {
      Name: this.SkillName.Language3,
      ParentSkill: this.SkillName.Language,
      Description: 'The ability to read/write this language.',
    } as Subskill,
    {
      Name: this.SkillName.Language4,
      ParentSkill: this.SkillName.Language,
      Description: 'The ability to read/write this language.',
    } as Subskill,
    {
      Name: this.SkillName.Language5,
      ParentSkill: this.SkillName.Language,
      Description: 'The ability to read/write this language.',
    } as Subskill,
    {
      Name: this.SkillName.Leadership,
      Description: 'The Leadership skill is for directing, inspiring, and rallying allies and comrades. A Traveller may make a Leadership action in combat.'
    },
    {
      Name: this.SkillName.Mechanic,
      Description: 'The mechanic skill allows a Traveller to maintain and repair most equipment - some advanced equipment and spacecraft components require the Engineer skill. Unlike the narrower and more focussed Engineer or Science skills, Mechanic does not allow a Traveller to build new devices or alter existing ones - it is purely for repairs and maintenance, but covers all types of equipment.'
    },
    {
      Name: this.SkillName.Medic,
      Description: 'The Medic skill covers emergency first aid and battlefield triage, as well as diagnosis, treatment, surgery, and long-term care.'
    },
    {
      Name: this.SkillName.Melee,
      Description: 'The Melee skill covers attacking in hand-to-hand combat and the use of suitable weapons.',
      Subskills: [this.SkillName.MeleeUnarmed, this.SkillName.MeleeBlade, this.SkillName.MeleeBludgeon]
    } as BaseSkill,
    {
      Name: this.SkillName.MeleeUnarmed,
      ParentSkill: this.SkillName.Melee,
      Description: 'Punching, kicking, wrestling, using improvised weapons in a bar brawl, etc. Also includes weapons that are part of an alien or creature, such as claws or teeth.',
    } as Subskill,
    {
      Name: this.SkillName.MeleeBlade,
      ParentSkill: this.SkillName.Melee,
      Description: 'Attacking with swords, rapiers, blades, and other edged weapons.',
    } as Subskill,
    {
      Name: this.SkillName.MeleeBludgeon,
      ParentSkill: this.SkillName.Melee,
      Description: 'Attacking with maces, clubs, staves, etc.',
    } as Subskill,
    {
      Name: this.SkillName.Navigation,
      Description: 'Navigation is the planetside counterpart of astrogation, covering plotting courses and finding directions on the ground.'
    },
    {
      Name: this.SkillName.Persuade,
      Description: 'Persuade is a more casual, informal version of Diplomat. It covers fast-talking, bargaining, wheedling, and bluffing. It also covers bribery or intimidation.'
    },
    {
      Name: this.SkillName.Pilot,
      Description: 'The Pilot skill specialities cover different forms of spacecraft.',
      Subskills: [this.SkillName.PilotSmallCraft, this.SkillName.PilotSpacecraft, this.SkillName.PilotCapitalShips]
    } as BaseSkill,
    {
      Name: this.SkillName.PilotSmallCraft,
      ParentSkill: this.SkillName.Pilot,
      Description: 'Shuttles and other craft under 100 tons.',
    } as Subskill,
    {
      Name: this.SkillName.PilotSpacecraft,
      ParentSkill: this.SkillName.Pilot,
      Description: 'Trade ships and other vessels between 100 and 5,000 tons.',
    } as Subskill,
    {
      Name: this.SkillName.PilotCapitalShips,
      ParentSkill: this.SkillName.Pilot,
      Description: 'Battleships and other ships over 5,000 tons.',
    } as Subskill,
    {
      Name: this.SkillName.Profession,
      Description: 'A Traveller with a Profession skill is trained in producing useful goods or services. There are many different Profession specialities, but each one works the same way - the Traveller can make a Profession check to earn money on a planet that supports that trade. The amount of money raised is Cr250 * the Effect of the check per month. Unlike other skills with specialities, levels in the Profession skill do not grant the ability to use other specialities at level 0. Each speciality must be learned individually. Someone with a Profession skill of 0 has a general grasp of working for a living, but little experience beyond the most menial jobs.',
      Subskills: [this.SkillName.Profession1, this.SkillName.Profession2, this.SkillName.Profession3, this.SkillName.Profession4, this.SkillName.Profession5]
    } as BaseSkill,
    {
      Name: this.SkillName.Profession1,
      ParentSkill: this.SkillName.Profession,
      Description: 'The ability to make money from this profession.',
    } as Subskill,
    {
      Name: this.SkillName.Profession2,
      ParentSkill: this.SkillName.Profession,
      Description: 'The ability to make money from this profession.',
    } as Subskill,
    {
      Name: this.SkillName.Profession3,
      ParentSkill: this.SkillName.Profession,
      Description: 'The ability to make money from this profession.',
    } as Subskill,
    {
      Name: this.SkillName.Profession4,
      ParentSkill: this.SkillName.Profession,
      Description: 'The ability to make money from this profession.',
    } as Subskill,
    {
      Name: this.SkillName.Profession5,
      ParentSkill: this.SkillName.Profession,
      Description: 'The ability to make money from this profession.',
    } as Subskill,
    {
      Name: this.SkillName.Recon,
      Description: 'A Traveller trained in Recon is able to scout out dangers and spot threats, unusual objects, or out of place people.'
    },
    {
      Name: this.SkillName.Science,
      Description: 'The Science skill covers not just knowledge, but also practical application of that knowledge where such practical application is possible.',
      Subskills: [this.SkillName.ScienceArchaeology, this.SkillName.ScienceAstronomy, this.SkillName.ScienceBiology,
        this.SkillName.ScienceChemistry, this.SkillName.ScienceCosmology, this.SkillName.ScienceCybernetics,
        this.SkillName.ScienceEconomics, this.SkillName.ScienceGenetics, this.SkillName.ScienceHistory,
        this.SkillName.ScienceLinguistics, this.SkillName.SciencePhilosophy, this.SkillName.SciencePhysics,
        this.SkillName.SciencePlanetology, this.SkillName.SciencePsionicology, this.SkillName.SciencePsychology,
        this.SkillName.ScienceRobotics, this.SkillName.ScienceSophontology, this.SkillName.ScienceXenology]
    } as BaseSkill,
    {
      Name: this.SkillName.ScienceArchaeology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of ancient civilizations. It also covers techniques of investigation and excavation.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceAstronomy,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of stars and celestial phenomena.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceBiology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of living organisms.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceChemistry,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of matter at the atomic, molecular, and macromolecular levels.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceCosmology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of the universe and its creation.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceCybernetics,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of blending living and synthetic life.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceEconomics,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of trade and markets.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceGenetics,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of genetic codes and engineering.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceHistory,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of the past, as seen through documents and records as opposed to physical artefacts.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceLinguistics,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of languages.',
    } as Subskill,
    {
      Name: this.SkillName.SciencePhilosophy,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of beliefs and religions.',
    } as Subskill,
    {
      Name: this.SkillName.SciencePhysics,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of fundamental forces.',
    } as Subskill,
    {
      Name: this.SkillName.SciencePlanetology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of planet formation and evolution.',
    } as Subskill,
    {
      Name: this.SkillName.SciencePsionicology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of psionic powers and phenomena.',
    } as Subskill,
    {
      Name: this.SkillName.SciencePsychology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of thought and society.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceRobotics,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of robot construction and use.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceSophontology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of intelligent living creatures.',
    } as Subskill,
    {
      Name: this.SkillName.ScienceXenology,
      ParentSkill: this.SkillName.Science,
      Description: 'The study of alien life forms.',
    } as Subskill,
    {
      Name: this.SkillName.Seafarer,
      Description: 'The Seafarer skill covers all manner of watercraft and ocean travel.',
      Subskills: [this.SkillName.SeafarerOceanShips, this.SkillName.SeafarerPersonal, this.SkillName.SeafarerSail, this.SkillName.SeafarerSubmarine]
    } as BaseSkill,
    {
      Name: this.SkillName.SeafarerOceanShips,
      ParentSkill: this.SkillName.Seafarer,
      Description: 'For motorised sea-going vessels.',
    } as Subskill,
    {
      Name: this.SkillName.SeafarerPersonal,
      ParentSkill: this.SkillName.Seafarer,
      Description: 'Used for very small waterborne craft such as canoes and rowboats.',
    } as Subskill,
    {
      Name: this.SkillName.SeafarerSail,
      ParentSkill: this.SkillName.Seafarer,
      Description: 'This skill is for wind-driven watercraft.',
    } as Subskill,
    {
      Name: this.SkillName.SeafarerSubmarine,
      ParentSkill: this.SkillName.Seafarer,
      Description: 'For vehicles that travel underwater.',
    } as Subskill,
    {
      Name: this.SkillName.Stealth,
      Description: 'A Traveller trained in the Stealth skill is adept at staying unseen, unheard, and unnoticed.'
    },
    {
      Name: this.SkillName.Steward,
      Description: 'The Steward skill allows the Traveller to serve and care for nobles and high-class passengers. It covers everything from proper address and behaviour, to cooking and tailoring, as well as basic management skills. A Traveller with the Steward skill is necessary on any ship offering High Passage.'
    },
    {
      Name: this.SkillName.Streetwise,
      Description: 'A Traveller with the Streetwise skill understands the urban environment and the power structures in society. On his homeworld and in related systems, he knows criminal contacts and fixers. On other worlds, he can quickly intuit power structures and can fit into the local underworld.'
    },
    {
      Name: this.SkillName.Survival,
      Description: 'The Survival skill is the wilderness counterpart of the urban Streetwise skill - The Traveller is trained to survive in the wild, build shelters, hunt or trap animals, avoid exposure, and so forth. He can recognize plants and animals of his homeworld and related planets, and can pick up on common clues and traits even on unfamiliar worlds.'
    },
    {
      Name: this.SkillName.Tactics,
      Description: 'This skill covers planning and decision making, from board games to squad level combat to fleet engagements.',
      Subskills: [this.SkillName.TacticsMilitary, this.SkillName.TacticsNaval]
    } as BaseSkill,
    {
      Name: this.SkillName.TacticsMilitary,
      ParentSkill: this.SkillName.Tactics,
      Description: 'Coordinating the attacks of foot troops or vehicles on the ground.',
    } as Subskill,
    {
      Name: this.SkillName.TacticsNaval,
      ParentSkill: this.SkillName.Tactics,
      Description: 'Coordinating the attacks of a spacecraft or fleet.',
    } as Subskill,
    {
      Name: this.SkillName.VaccSuit,
      Description: 'The Vacc Suit skill allows a Traveller to wear and operate spacesuits and environmental suits. A Traveller will rarely need to make Vacc Suit checks under ordinary circumstances - merely possessing the skill is enough. If the Traveller does not have the requisite Vacc Suit skill for the suit he is wearing, he suffers DM-2 to all skill checks made while wearing a suit for each missing level. This skill also permits the character to operate advanced battle armour.'
    },
  ];
  SkillNames = ['Admin', 'Advocate', 'Animals', 'Handling', 'Training', 'Veterinary', 'Art', 'Performer', 'Holography',
    'Instrument', 'Visual Media', 'Write', 'Astrogation', 'Athletics', 'Strength', 'Dexterity', 'Endurance', 'Broker',
    'Carouse', 'Deception', 'Diplomat', 'Drive', 'Hovercraft', 'Mole', 'Track', 'Walker', 'Wheel', 'Electronics', 'Comms',
    'Computers', 'Remote Ops', 'Sensors', 'Engineer', 'M-Drive', 'J-Drive', 'Life Support', 'Power', 'Explosives',
    'Flyer', 'Airship', 'Grav', 'Ornithopter', 'Rotor', 'Wing', 'Gambler', 'Gunner', 'Turret', 'Ortillery', 'Screen',
    'Capital', 'Gun Combat', 'Archaic', 'Energy', 'Slug', 'Heavy Weapons', 'Artillery', 'Man Portable', 'Vehicle',
    'Investigate', 'Jack-of-All-Trades', 'Language', 'Language1', 'Language2', 'Language3', 'Language4', 'Language5',
    'Leadership', 'Mechanic', 'Medic', 'Melee', 'Unarmed', 'Blade', 'Bludgeon', 'Navigation', 'Persuade', 'Pilot',
    'Small Craft', 'Spacecraft', 'Capital Ships', 'Profession', 'Profession1', 'Profession2', 'Profession3', 'Profession4',
    'Profession5', 'Recon', 'Science', 'Archaeology', 'Astronomy', 'Biology', 'Chemistry', 'Cosmology', 'Cybernetics',
    'Economics', 'Genetics', 'History', 'Linguistics', 'Philosophy', 'Physics', 'Planetology', 'Psionicology', 'Psychology',
    'Robotics', 'Sophontology', 'Xenology', 'Seafarer', 'Ocean Ships', 'Personal', 'Sail', 'Submarine', 'Stealth',
    'Steward', 'Streetwise', 'Survival', 'Tactics', 'Military', 'Naval', 'Vacc Suit'];

  constructor() {
  }

  getSkill(skillName: string): Skill {
    let skill = this.skills.find(x => x.Name == skillName);
    return skill ? skill : {} as Skill;
  }

  getDescription(skillName: string): string {
    let skill = this.skills.find(x => x.Name == skillName);
    let Description = '';
    if (skill) {
      Description = skill.Description;
    }
    return Description ? Description : '';
  }

  getGroups(skills: string[]): Record<string, string[]> {
    let groups = {} as Record<string, string[]>;
    for (let skillName of skills) {
      let skill = this.getSkill(skillName);
      if ((skill as Subskill).ParentSkill) {
        let subskill = skill as Subskill;
        if (groups[subskill.ParentSkill]) {
          groups[subskill.ParentSkill].push(subskill.Name);
        } else {
          groups[subskill.ParentSkill] = [subskill.Name];
        }
      } else if ((skill as BaseSkill).Subskills) {
        let baseSkill = skill as BaseSkill;
        if (!groups[baseSkill.Name]) {
          groups[baseSkill.Name] = [];
        }
      } else {
        groups[skill.Name] = [skill.Name];
      }
    }
    return groups;
  }

  getGroupNames(skills: string[]): string[] {
    let groupNames = [] as string[];

    for (let skillName of skills) {
      let skill = this.getSkill(skillName);
      if ((skill as Subskill).ParentSkill) {
        let subskill = skill as Subskill;
        if (groupNames.indexOf(subskill.ParentSkill) < 0) {
          groupNames.push(subskill.ParentSkill);
        }
      } else if ((skill as BaseSkill).Subskills) {
        let baseSkill = skill as BaseSkill;
        if (groupNames.indexOf(baseSkill.Name) < 0) {
          groupNames.push(skill.Name);
        }
      } else {
        if (groupNames.indexOf(skill.Name) < 0) {
          groupNames.push(skill.Name);
        }
      }
    }
    return groupNames;
  }

  getBasicGroups(skills: string[]): Record<string, string[]> {
    let groups = {} as Record<string, string[]>;
    groups['Basic Skills'] = [];

    for (let skillName of skills) {
      let skill = this.getSkill(skillName);

      if ((skill as Subskill).ParentSkill) {
        // Do nothing
      } else {
        groups['Basic Skills'].push(skill.Name);
      }
    }

    return groups;
  }
}
