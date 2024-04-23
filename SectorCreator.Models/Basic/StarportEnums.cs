namespace SectorCreator.Models.Basic;

public enum StarportClass
{
    A,
    B,
    C,
    D,
    E,
    X,
    F,
    G,
    H,
    Y
}

public enum StarportType
{
    Starport,
    Spaceport
}

public enum StarportEnforcement
{
    NoStarport,
    None,
    Violent,
    Harmonious,
    Minimal,
    Small,
    Average,
    WellEquipped,
    Paramilitary,
    Military,
    PrivateArmy
}

public enum StarportDefenses
{
    NoStarport,
    None,
    Minimal,
    Light,
    Standard,
    Heavy,
    VeryHeavy,
    Fortress
}

public enum StarportEvent
{
    None,
    Embargo,
    Neglected,
    Secure,
    RunDown,
    Slowdown,
    VibrantMarket,
    TrafficSurge,
    Investment,
    NewMarket,
    Smuggling,
    Expansion
}

public enum StarportSpecialistType
{
    None,
    Industrial,
    LaunchFacilities,
    Makeshift,
    Military,
    Passenger,
    Private
}

public enum StarportInstallation
{
    ArmyBase,
    DefenseBase,
    MaintenanceFacility,
    NavalBase,
    NavalDepot,
    ResearchFacility,
    ScoutBase,
    ExplorerBase,
    Shipyard,
    WayStation,
    TAS,
    MegaCorporateHeadquarters
}

public enum StarportSpecialFeature
{
    None,
    Ruins,
    AlienQuarter,
    LacksCapability,
    NoDownport,
    SpecialistType,
    Installation,
    Independent,
    RunByEntity,
    MajorTradeNexus,
    Orbital,
    UnusualConstruction
}