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

### APIs
Catalog Service API - http://host.docker.internal:8000

### GUIs
Portainer - http://host.docker.internal:9000  
Mongo GUI - http://host.docker.internal:8081  
Kibana - http://host.docker.internal:5601

### Services
Catalog Database - http://host.docker.internal:27017  
Catalog API Redis - http://host.docker.internal:6379  
Gateway - http://host.docker.internal:8010  
Elasticsearch - http://host.docker.internal:9200  

### HealthChecks
Web App - http://host.docker.internal:8007  