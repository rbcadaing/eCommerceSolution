az acr create --resource-group aks-demo-rg --name ecommerceregistry --sku free


docker tag apigateway:latest ecommerceregistry.azurecr.io/apigateway:latest
docker tag users-microservice:latest ecommerceregistry.azurecr.io/users-microservice:latest
docker tag orders-microservice:latest ecommerceregistry.azurecr.io/orders-microservice:latest
docker tag products-microservice:latest ecommerceregistry.azurecr.io/products-microservice:latest
docker tag ecommerce-mysql:latest ecommerceregistry.azurecr.io/ecommerce-mysql:latest
docker tag ecommerce-mongodb:latest ecommerceregistry.azurecr.io/ecommerce-mongodb:latest
docker tag ecommerce-postgres:latest ecommerceregistry.azurecr.io/ecommerce-postgres:latest

docker push ecommerceregistry.azurecr.io/apigateway:latest
docker push ecommerceregistry.azurecr.io/users-microservice:latest
docker push ecommerceregistry.azurecr.io/orders-microservice:latest
docker push ecommerceregistry.azurecr.io/products-microservice:latest
docker push ecommerceregistry.azurecr.io/ecommerce-mysql:latest
docker push ecommerceregistry.azurecr.io/ecommerce-mongodb:latest
docker push ecommerceregistry.azurecr.io/ecommerce-postgres:latest

az acr create --resource-group aks-demo-rg --name ecommerceregistry --sku basic

az aks get-credentials --resource-group aks-demo-rg --name ecommerce-aks-cluster

