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
## Local/Docker Development
`5000/8000 - Catalog API`  
`5007/8007 - Health Check`    
`5010/8010 - API Gateway`  
`5011/8011 - Identity Server`  

## Reserved Ports for Docker
`5601 - Kibana GUI`  
`6779 - Redis for Catalog`  
`8081 - Mongo GUI`  
`9000 - Portainer`  
`9200 - Elasticsearch`  
`27017 - Mongo Database for Catalog`  

# Quick Links for Docker
## APIs
Catalog Service API - http://host.docker.internal:8000

## GUIs
Portainer - http://host.docker.internal:9000  
Mongo GUI - http://host.docker.internal:8081  
Kibana - http://host.docker.internal:5601

## Services
Catalog Database - http://host.docker.internal:27017  
Catalog API Redis - http://host.docker.internal:6379  
Gateway - http://host.docker.internal:8010  
Elasticsearch - http://host.docker.internal:9200  

## HealthChecks
Web App - http://host.docker.internal:8007  