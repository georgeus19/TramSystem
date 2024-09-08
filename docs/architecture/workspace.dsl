workspace {

    model {

        user = person "User" "Plans Missions"

        softwareSystem = softwareSystem "TramSystem" "Tram software system." {

            frontendContainer = container "Frontend" "Provides UI for adding trams and planning missions." "Blazor"
            depotContainer = container "Depot Service" "Service for interacting with depot and its trams." "ASP.NET Core"
            missionPlanningContainer = container "Mission Planning Service" "Service for planning missions." "ASP.NET Core"
            depotStorageContainer = container "Depot Storage" "Storage for depot data." "Redis"
            missionStorageContainer = container "Mission Planning Storage" "Storage for missions." "Redis"
        }

        user -> frontendContainer
        frontendContainer -> depotContainer "Access depot"
        frontendContainer -> missionPlanningContainer "Plan missions"

        missionPlanningContainer -> depotContainer "Access depot"

        depotContainer -> depotStorageContainer
        missionPlanningContainer -> missionStorageContainer


    }

    views {

        container softwareSystem "SystemContainerView" {
            include *
        }

        

     styles {
        element "Element" {
            fontSize 32
            color #01204E
            background #999999
        }

        relationship "Relationship" {
            fontSize 30
        }

        element "Group" {
            fontSize 30
        }

        element "Analyzer" {

        }
    }

    theme default

    }
    
}