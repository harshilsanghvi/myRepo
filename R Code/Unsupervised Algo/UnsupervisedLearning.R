rm(list = ls())
USArrest <- read.csv("C:Users/HS/Desktop/BDAP/ML/ML102/protein.csv",header = T)
?par
install.packages("arules")
library(Matrix)

############# CLUSTERING ###########

############# Clu: Kmeans Algorithm #############
## European protein consumption
#read in the data
food <- read.csv('C:/Users/HS/Desktop/BDAP/ML/ML102/protein.csv')
food[1:3,]
set.seed(1)
grpProtein <- kmeans(food[,-1],centers = 5, nstart = 5)
# grpProtein <- kmeans(food[,c("WhiteMeat","RedMeat")],centers = 10, nstart = 10)
#list of cluster assignments
o=order(grpProtein$cluster)
data.frame(food$Country[o],grpProtein$cluster[o])

install.packages('cluster')
library(cluster)
food <- read.csv('C:/Users/HS/Desktop/BDAP/ML/ML102/protein - Copy.csv')

#euclidean distance and average linkage
foodagg = agnes(food,diss=FALSE, metric='euclidean')
?agnes
# agglomerative nesting
# diss - logical flag: if TRUE (default for dist or dissimilarity objects), 
# then x is assumed to be a dissimilarity matrix. If FALSE, then x 
# is treated as a matrix of observations by variables
# metric - 'euclidean and manhattan' 
# Euclidean distances are root sum-of-squares of differences, and
# manhattan distances are the sum of absolute differences. 
# If x is already a dissimilarity matrix, then this argument will 
# be ignored.

plot(foodagg)#dendogram

############# Clu: Hierarchical Algorithm #############
library(cluster)
food <- read.csv('C:/Users/HS/Desktop/BDAP/ML/ML102/protein - Copy.csv')

#Euclidean distance and the single linkage
foodaggsin = agnes(food,diss=FALSE, metric='euclidean',method='single')
plot(foodaggsin)#dendogram

foodaggcomp = agnes(food,diss=FALSE, metric='euclidean',method='complete')
plot(foodaggcomp)#dendogram

buyers <- read.csv('C:/Users/HS/Desktop/BDAP/ML/ML102/Buy.csv')
buyers.data <- buyers[,-1]
#buyers.columns <- c('id','Budget','Posession','Bhk','Amenities','Project type')
sd.data=scale(buyers.data)
par(mfrow=c(1,3))
data.dist=dist(sd.data)
plot(hclust(data.dist,method='average'),labels=buyers$id,main="Average Linkage",xlab="",sub="",ylab="")
plot(hclust(data.dist,method='single'),labels=buyers$id,main="Single Linkage",xlab="",sub="",ylab="")
plot(hclust(data.dist,method='complete'),labels=buyers$id,main="Complete Linkage",xlab="",sub="",ylab="")


############# ASSOCIATION #########

############# Aso: Apriori Algorithm #############
lastfm <- read.csv("C:/Users/HS/Desktop/BDAP/ML/ML102/lastfm.csv",header = T)
length(lastfm$user)
lastfm$user <- as.factor(lastfm$user) # coerced users as factors
class(lastfm$user)
#a-rules package for association rules
library(arules)#computation environment for mining association rules and finding the item sets
library(Matrix)

#preprocess the data before applying arules algo

playlist <- split(x=lastfm[,"artist"],f = lastfm$user)
playlist <- lapply(playlist,unique) # remove the duplicates

playlist <- as(playlist,"transactions")
#view this as a list of transactions
#transaction is a data class defined in arules

itemFrequency(playlist)
itemFrequencyPlot(playlist,support=0.08, cex.names = 0.5)
musicrules <- apriori(playlist,parameter = list(support=.01,confidence=.5))
inspect(musicrules)

############# Aso: PCA #####

states = row.names(USArrests)
states
USArrests
USArrest <- read.csv("C:/Users/HS/Desktop/BDAP/ML/ML102/protein.csv",header = T)
names(USArrest)
apply(USArrest[,-1],2,mean)
apply(USArrest[,-1],2,var)
help.search("apply")
pr.out = prcomp(USArrest[,-1],scale=TRUE)
names(pr.out)
pr.out$center
pr.out$scale
pr.out$rotation
dim(pr.out$x)
biplot(pr.out,scale=0,choices = c(1,2))
pr.out$rotation=-pr.out$rotation
pr.out$x=-pr.out$x
biplot(pr.out,scale=0)
pr.out$sdev
pr.var=pr.out$sdev^2
pr.var
pve=pr.var/sum(pr.var)
pve
plot(pve,xlab="Principal Component", ylab = "Proportion of Variamce Explained",ylim=c(0,1),type="b")
plot(cumsum(pve),xlab="Principal Component",ylab="Cumulative Proportion",ylm=c(0,1),type="b")