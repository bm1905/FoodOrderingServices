# FoodOrderingServices
APIs for Food Ordering Service

# Embeded Diagrams
![Alt text here](SAD.png)

# General Commands

## Docker Up
`docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d`

## Docker Down
`docker-compose -f docker-compose.yml -f docker-compose.override.yml down`

## Docker Build
`docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build`

# Ports Usage
Portainer `:9000`
Mongo GUI `:8081`

Catalog Datbase `:27017`
Catalog Service `:8000`