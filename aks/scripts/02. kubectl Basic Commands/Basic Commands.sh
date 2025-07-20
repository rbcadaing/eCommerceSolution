# Get list of nodes in your cluster:
    kubectl get nodes

#   This should return a list of nodes in your AKS cluster.
#   We've created the cluster with two worker nodes.



# Get list of namespaces in your cluster:
    kubectl get namespaces

#   Lists all namespaces. Namespaces provide a way to divide cluster resources between multiple users or teams.
#   You will have to create one namespace for each set of resources (such as Pods) that need to be managed by a user or a team.

#   Namespaces in Kubernetes are a way to divide a single cluster into multiple virtual clusters. 
    
#   They help organize and manage resources, allowing different teams or applications to share the same cluster without interfering with each other. 
    
#   By using namespaces, you can isolate resources like pods and services, set resource quotas, and apply policies separately for each namespace. This makes it easier to manage and secure resources within a large, shared Kubernetes environment.

