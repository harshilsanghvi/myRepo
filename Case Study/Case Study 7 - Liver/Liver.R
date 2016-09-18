library(tidyr)

data1 <-read.csv("C:/Users/HS/Desktop/BDAP/Case Study/Case Study 7 - Liver/liver.csv")
head(data1)
ndata <- na.omit(data1)
logr <- glm(target ~ ., data=ndata, family=binomial(link="logit"))
summary(logr)


# KNN

#for illustration consider the ri*ai plane
#evaluation (test, hold-out) cases
n=length(ndata$target)
nt=200
set.seed(1)
#to make the calculations reproducible in repeated rns
train <- sample(1:n,nt)
# Standardization of the data is preferable, especially if 
# units of the features are quite different

x <- scale(ndata[,c(4,1)])
x[1:3,]

library(class)

nearest1 <- knn(train=x[train,],test=x[-train,], cl=ndata$target[train],k=1)
nearest5 <- knn(train=x[train,],test=x[-train,], cl=ndata$target[train],k=5)
data.frame(ndata$target[-train],nearest1,nearest5)

par(mfrow=c(1,2))
#plot for k = 1

plot(x[train,], col = ndata$target[train],cex=.8,main="1-nearest neighbour")
points(x[-train],bg=nearest1,pch=21,col=grey(.9),cex=1.25)
# plot for k=5
plot(x[train,], col = ndata$target[train],cex=.8,main="5-nearest neighbour")
points(x[-train],bg=nearest5,pch=21,col=grey(.9),cex=1.25)

legend("topright",legend=levels(ndata$target), fill=1:6,bty="n",cex = .75)
#calculate the proportion of correct classifications on this one 
pcorrn1=100*sum(ndata$target[-train]==nearest1)/(n-nt)
pcorrn5=100*sum(ndata$target[-train]==nearest5)/(n-nt)
pcorrn1
pcorrn5

x <- scale(ndata[,c(1:9)])

nearest1 <- knn(train=x[train,],test=x[-train,], cl=ndata$target[train],k=1)
nearest5 <- knn(train=x[train,],test=x[-train,], cl=ndata$target[train],k=3)
#calculate the proportion of correct classifications on this one 
pcorrn1=100*sum(ndata$target[-train]==nearest1)/(n-nt)
pcorrn5=100*sum(ndata$target[-train]==nearest5)/(n-nt)

pcorrn1
pcorrn5

# DECISION TREE

plot(accel~times,data = mcycle)
# plot(mcycle$times,mcycle$accel) above line does similar thing to this 

mct <-tree(accel~times, data=mcycle)
?tree
# A tree is grown by binary recursive partitioning using the response
# in the specified formula and choosing splits from the terms of the right-hand-side.
class(mct) # tree
mct
plot(mct, col=8)
text(mct, cex=.75)

library(rpart)
kyphosis
fit<-rpart(Kyphosis ~Age + Number + Start, method="class", data=kyphosis)
?rpart
# recursive partitioning and regression trees 
# method = one of "anova", "poisson", "class" or "exp". If method is missing 
# then the routine tries to make an intelligent guess. If y is a 
# survival object, then method = "exp" is assumed, if y has 2 columns 
# then method = "poisson" is assumed, if y is a factor then 
# method = "class" is assumed, otherwise method = "anova" is assumed. 
# It is wisest to specify the method directly,
# especially as more criteria may added to the function in future.
fit
class(fit) #class is rpart

plot(fit, uniform=TRUE, main="Classification Tree")
# above plot means plot.rpart()
?uniform #search uniform in plot.rplot
# uniform in rpart plot means if TRUE, uniform vertical spacing of 
# the nodes is used; this may be less cluttered when fitting a large
# plot onto a page. The default is to use anon-uniform spacing
# proportional to the error in the fit.

text(fit, use.n=TRUE, all=TRUE, cex=.8)
?text.rpart
# Labels the current plot of the tree dendrogram with text
# use.n = Logical. If TRUE, adds to label (\#events level1/ \#events
# level2/etc. for class, n for anova, and \#events/n for poisson 
# and exp).
# Logical. If TRUE, all nodes are labeled, 
# otherwise just terminal nodes.