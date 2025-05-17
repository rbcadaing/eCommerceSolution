## Docker build loca image
docker build -t users-microservice:1.0 -f ./eCommerce.API/Dockerfile .

## Docker Hub Login

docker login -u rbcadaing -p dckr_pat_zsURbVJoOuGpxkNd4bqsQzmRPME

## Tag docker image
docker tag users-microservice:1.0 rbcadaing/ecommerce-users-microservice:v1.0

docker push rbcadaing/ecommerce-users-microservice:v1.0

## Run the docker container

docker run -d -p 8080:80 --name users-microservice rbcadaing/ecommerce-users-microservice:v1.0
