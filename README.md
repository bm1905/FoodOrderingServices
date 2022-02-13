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

### Portainer
[http://host.docker.internal:9000](http://host.docker.internal:9000)
### Mongo GUI
[http://host.docker.internal:8081](http://host.docker.internal:8081)
### Catalog Database
:27017
### Catalog Service
[http://host.docker.internal:8000](http://host.docker.internal:8000)
### Gateway
http://host.docker.internal:8010
### Elasticsearch
http://host.docker.internal:9200
### Kibana
[http://host.docker.internal:5601](http://host.docker.internal:5601)