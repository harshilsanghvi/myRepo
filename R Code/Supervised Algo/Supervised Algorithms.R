rm(list = ls())
USArrest <- read.csv("C:Users/HS/Desktop/BDAP/ML/ML102/protein.csv",header = T)
?par
install.packages("arules")
library(Matrix)
######################## REGRESSION ###########################


############# Reg: LM - Numerical values #####################

toyota <- read.csv("C:/Users/HS/Desktop/BDAP/ML/Toyota.csv")
toyota[1:3,]

v1 = rep(1,length(toyota$FuelType))
v2 = rep(0,length(toyota$FuelType))
toyota$FuelType1=ifelse(toyota$FuelType=="CNG",v1,v2)
toyota$FuelType2=ifelse(toyota$FuelType=="Diesel",v1,v2)
auto=toyota[-4]
auto[1:3,]
auto
plot(Price~Age,data=auto)
plot(Price~KM,data=auto)
plot(Price~HP,data=auto)
plot(Price~MetColor,data=auto)
plot(Price~Automatic,data=auto)
plot(Price~CC,data=auto)
plot(Price~Doors,data=auto)
plot(Price~Weight,data=auto)

## regression on all types
m1 = lm(Price~ Age + KM + HP + CC + Weight,data=auto)
summary(m1)

############# Reg: GLM - Numberical values #####

# Build the model where response variable is Personal Loan as a function of all other variable
# Few needs preprocessing

# CCAverage - Average Monthly Credit Card Spending

# install.packages("car")
library(car) ## needed to recode variables
set.seed(1)

loan <- read.csv("C:/Users/Amit/BDAP/Module 13 - Machine Learning/Universal Bank/UniversalBank.csv")
loan[1:3,]

## creating indicator variables for loan$Family and loan$Education
v1=rep(1,dim(loan)[1])
v2=rep(0,dim(loan)[1])

## creating indicator variables for Family Size (4 groups: 1,2,3,4)
loan$FamSize2=ifelse(loan$Family==2,v1,v2)
loan$FamSize3=ifelse(loan$Family==3,v1,v2)
loan$FamSize4=ifelse(loan$Family==4,v1,v2)

## creating indicator variables for Education (3 groups: 1,2,3)
loan$Educ2=ifelse(loan$Education==2,v1,v2)
loan$Educ3=ifelse(loan$Education==3,v1,v2)

response=loan$PersonalLoan

xx=cbind(response,Age=loan$Age,Exp=loan$Experience,Inc=loan$Income,Fam2=loan$FamSize2,Fam3=loan$FamSize3,Fam4=loan$FamSize4,CCAve=loan$CCAvg,Mort=loan$Mortgage,SecAcc=loan$SecuritiesAccount,CD=loan$CDAccount,Online=loan$Online,CreditCard=loan$CreditCard,Educ2=loan$Educ2,Educ3=loan$Educ3)
xx[1:3,]

# split the dataset into training and test evaluation set
# 5000 ==> 3000 training ; 2000 test

n=dim(loan)[1]
n1=floor(n*(0.6))
n2=n-n1
train=sample(1:n,n1)

xx=xx[,-1]
xtrain <- xx[train,]
xnew <- xx[-train,]

ytrain <- response[train]
ynew <- response[-train]

## model fitted on the training dataset
m2=glm(response~.,family=binomial, data=data.frame(response=ytrain,xtrain))
summary(m2)
?glm
# create predictions for the test (evaulation) data set
ptest=predict(m2,newdata=data.frame(xnew),type="response")
ptest

##coding as 1 if probability 0.5 or larger

gg1=floor(ptest+0.5)
ttt=table(ynew,gg1)
ttt
error=(ttt[1,2]+ttt[2,1])/n2
error

############# Reg: LR - Binary Val #############
LR <- read.csv("C:/Users/HS/Desktop/BDAP/ML/computer.csv")
LR
#logr <- glm(Loan.approval ~ Salary+Age, data=LR, family=binomial(link = "logit"))
logr <- glm(B1 ~ A1+A2+I1+I2+S1+CR1, data=LR, family=binomial(link = "logit"))
logr
summary(logr)

############# Reg: MLR : Multi val #############

install.packages("nnet")

library(nnet)
data <- read.csv("C:/Users/HS/Desktop/BDAP/ML/loanappve_mnlr.csv")
# Regression model
Model <- multinom(Loan.approval~.,data = data)
?multinom
summary(Model)
# the Coefficients W , Y has intercept, feature1,feature2 
# Each coefficient values create an equation p(w)+p(y)+p(n)=1 

############# SVM : Regression  #############

library(e1071)
set.seed(1)
#generating data set
x=matrix(rnorm(20*2),ncol=2)
y=c(rep(-1,10),rep(1,10))
x[y==1,]=x[y==1,]+1

#pot original data
plot(x,col=(3-y)) 

#combine variable into a dataframe
dat <- data.frame(x=x,y=as.factor(y))
#run SVM algorith
svmfit <- svm(y~.,data=dat, kernel="linear",cost=0.05,scale=FALSE)
#plot classified dataset
plot(svmfit,data=dat,x.2~x.1)


######################## CLASSIFICATION #######################


############# Naive Bayes Algorithm: Classification #############
install.packages("mlbench")
install.packages("e1071")
library(e1071)
library("mlbench")
data("HouseVotes84",package = "mlbench")
HouseVotes84
model <- naiveBayes(Class ~ .,data = HouseVotes84)
predict(model,HouseVotes84[1:10,])
predict(model,HouseVotes84[1:10,], type = "raw")
pred <- predict(model, HouseVotes84)
table(pred, HouseVotes84$Class)
pred

############# Decision Tree Algorithm: ML 101 Supervised - Classification #############
library(rpart)
kyphosis
fit<-rpart(Kyphosis ~Age + Number + Start, method="class", data=kyphosis)
plot(fit, uniform=TRUE, main="Classification Tree")
text(fit, use.n=TRUE, all=TRUE, cex=.8)

############# K nearest Algorithm: ML 101 Supervised - Classification #############
data(fgl)
fgl
n=length(fgl$type)
nt=200
set.seed(1)
#to make the calculations reproducible in repeated rns
train <- sample(1:n,nt)
x <- scale(fgl[,c(4,1)])
x[1:3,]
library(class)
nearest1 <- knn(train=x[train,],test=x[-train,], cl=fgl$type[train],k=1)
nearest5 <- knn(train=x[train,],test=x[-train,], cl=fgl$type[train],k=3)
data.frame(fgl$type[-train],nearest1,nearest5)
par(mfrow=c(1,2))
#plot for k = 1
plot(x[train,], col = fgl$type[train],cex=.8,main="1-nearest neighbour")
points(x[-train],bg=nearest1,pch=21,col=grey(.9),cex=1.25)
# plot for k=5
plot(x[train,], col = fgl$type[train],cex=.8,main="5-nearest neighbour")
points(x[-train],bg=nearest5,pch=21,col=grey(.9),cex=1.25)
legend("topright",legend=levels(fgl$type), fill=1:6,bty="n",cex = .75)
#calculate the proportion of correct classifications on this one 
pcorrn1=100*sum(fgl$type[-train]==nearest1)/(n-nt)
pcorrn5=100*sum(fgl$type[-train]==nearest5)/(n-nt)
pcorrn1
pcorrn5
x <- scale(fgl[,c(1:9)])
nearest1 <- knn(train=x[train,],test=x[-train,], cl=fgl$type[train],k=1)
nearest5 <- knn(train=x[train,],test=x[-train,], cl=fgl$type[train],k=5)
#calculate the proportion of correct classifications on this one 
pcorrn1=100*sum(fgl$type[-train]==nearest1)/(n-nt)
pcorrn5=100*sum(fgl$type[-train]==nearest5)/(n-nt)
pcorrn1
pcorrn5

