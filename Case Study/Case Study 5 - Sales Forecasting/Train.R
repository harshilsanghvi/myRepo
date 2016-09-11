install.packages("corrplot")
install.packages(("Boruta"))
# Boruta is used similar to PCA but it includes categorical features as well. In PCA, we have only numeric data
library(Boruta)
rm(list = ls())

# Read training set 
Train <- read.csv("C:/Users/HS/Desktop/BDAP/Case Study 13-8/10 Sept/training.csv")

# PLot Box graph
par(mfrow=c(3,3), mai = c(.3,.6,.1,.1))
boxplot(Train$Percent_SSC, data = Train)
boxplot(Train$Percent_HSC, data = Train)
boxplot(Train$Percent_Degree, data = Train)
boxplot(Train$Experience_Yrs, data = Train)
boxplot(Train$Percentile_ET, data = Train)
boxplot(Train$Percent_MBA, data = Train)
boxplot(Train$Marks_Communication, data = Train)
boxplot(Train$Marks_Projectwork, data = Train)
chck <- boxplot(Train$Marks_BOCA, data = Train)
sort(Train$Percent_SSC) 

# Get the outliers from the boxplot
outliers_Percent_HSC = boxplot(Train$Percent_SSC, plot = FALSE)$out
outliers_Percent_HSC

plot(Percent_SSC ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Percent_HSC ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Percent_Degree ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Experience_Yrs ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Percentile_ET ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Percent_MBA ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Marks_Communication ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Marks_Projectwork ~ Placement, data = Train, col=c(grey(.2),2:6))
plot(Marks_BOCA ~ Placement, data = Train, col=c(grey(.2),2:6))

# Remove all categorical variable and keep in DF
DF <- data.frame(Train$Percent_SSC,Train$Percent_HSC,Train$Percent_Degree,Train$Experience_Yrs,Train$Percentile_ET,Train$Percent_MBA,Train$Marks_Communication,Train$Marks_Projectwork,Train$Marks_BOCA)
Train <- data.frame(Train$Percent_SSC,Train$Percent_HSC,Train$Percent_Degree,Train$Experience_Yrs,Train$Percentile_ET,Train$Percent_MBA,Train$Marks_Communication,Train$Marks_Projectwork,Train$Marks_BOCA)

# Generate correlation matrix of numeric features
M <- cor(DF)
corrplot(M)

# Remove empty values from the Train dataframe. First put NA in that place. then delete it and save in Train
Train[Train == ""] <-  NA
Train <- Train[complete.cases(Train),]

# Convert categorical variable into numerical which used for Boruta analysis
convert <- c(1,3,5,6,8,10,13,17)
Train[,-convert] <- data .frame(apply(Train[convert],2,as.factor))

# Perform Boruta on the transformed data. Boruta input should be - all features cateogrical and no NA.
set.seed(123)
boruta.train <- Boruta(Placement~., data = Train, doTrace = 2)
print(boruta.train)
boruta.train

# perform unsupervised learning. Kmeans algo. remove all NA's
set.seed(1)
k <- na.omit(Train)
gTrain <- kmeans(k,centers = 3, nstart = 5)
o=order(gTrain$cluster)

# export data into csv
writes <- data.frame(Train,gTrain$cluster[o])
write.csv(writes, "C:/Users/HS/Desktop/BDAP/Case Study 13-8/10 Sept/data.csv")

install.packages("dummies")
library(dummies)
#logr <- glm(Loan.approval ~ Salary+Age, data=LR, family=binomial(link = "logit"))
Training <- read.csv("C:/Users/HS/Desktop/BDAP/Case Study 13-8/10 Sept/trainingLR_withoutCat.csv")
Train <- read.csv("C:/Users/HS/Desktop/BDAP/Case Study 13-8/10 Sept/trainingLR.csv")
Train$Place
str(Train)
Streams <- dummy(as.numeric(Train$Course_Degree))
Entrance <- dummy(as.numeric(Train$Entrance_Test))
dim(Entrance)
Trains <- data.frame(Train$Place,Training$Percent_SSC,Entrance)
Trainscheck <- Trains[0:200,]
TrainsTest <- Trains[201:303,]
TrainsTest <- TrainsTest[c(-1)]

dim(Trains)
str(TrainsTest)
str(Trains)
 logr <- glm(Train.Place ~ ., data=Trainscheck, family=binomial(link = "logit"))
logr
summary(logr)

predict(logr,newdata = TrainsTest, type = "response")
