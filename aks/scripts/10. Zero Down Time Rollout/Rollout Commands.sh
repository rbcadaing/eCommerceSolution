# Updating deployment:
    kubectl get deployments --namespace ecommerce-namespace

    kubectl rollout restart deployment mongodb-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment mysql-deployment --namespace ecommerce-namespace


    kubectl rollout restart deployment postgres-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment rabbitmq-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment redis-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment users-microservice-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment products-microservice-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment orders-microservice-deployment --namespace ecommerce-namespace

    kubectl rollout restart deployment apigateway-deployment --namespace ecommerce-namespace

