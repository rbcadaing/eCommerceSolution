## Docker Build Product Service
on the root solution folder run 
docker build -t products-microservice:1.0 -f ProductsMicroService.API/Dockerfile .

## Docker Hub Login

docker login -u rbcadaing -p dckr_pat_zsURbVJoOuGpxkNd4bqsQzmRPME

## Tag docker image
docker tag products-microservice:1.0 rbcadaing/ecommerce-products-microservice:v1.0

docker push rbcadaing/ecommerce-products-microservice:v1.0

## Run the docker container

docker run -d -p 8080:80 --name products-microservice rbcadaing/ecommerce-products-microservice:v1.0

