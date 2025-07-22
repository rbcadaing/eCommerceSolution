docker-compose -f docker-compose.build.yaml build

# -------------

# Tag:
docker tag orders-microservice:latest harshaecommerceregistry.azurecr.io/orders-microservice:latest
docker tag products-microservice:latest harshaecommerceregistry.azurecr.io/products-microservice:latest
docker tag users-microservice:latest harshaecommerceregistry.azurecr.io/users-microservice:latest
docker tag apigateway:latest harshaecommerceregistry.azurecr.io/apigateway:latest
docker tag ecommerce-mysql:latest harshaecommerceregistry.azurecr.io/ecommerce-mysql:latest
docker tag ecommerce-postgres:latest harshaecommerceregistry.azurecr.io/ecommerce-postgres:latest
docker tag ecommerce-mongodb:latest harshaecommerceregistry.azurecr.io/ecommerce-mongodb:latest

# ------------

# Push:
az acr login --name harshaecommerceregistry

docker push harshaecommerceregistry.azurecr.io/orders-microservice:latest
docker push harshaecommerceregistry.azurecr.io/products-microservice:latest
docker push harshaecommerceregistry.azurecr.io/users-microservice:latest
docker push harshaecommerceregistry.azurecr.io/apigateway:latest
docker push harshaecommerceregistry.azurecr.io/ecommerce-mysql:latest
docker push harshaecommerceregistry.azurecr.io/ecommerce-postgres:latest
docker push harshaecommerceregistry.azurecr.io/ecommerce-mongodb:latest


