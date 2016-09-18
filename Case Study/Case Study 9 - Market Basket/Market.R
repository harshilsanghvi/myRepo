mktbskt <- read.transactions("C:/Users/HS/Desktop/BDAP/Case Study/Case Study 9 - Market Basket/market.csv",sep=",", format="basket")

length(market)
class(market)

market <- split(market, seq(nrow(market))) # convert data frame into collection of lists

mktbskt <- read.transactions("/home/amit/bdap/CaseStudies/data/mktbasket.csv", sep=",", format="basket")
class(mktbskt)
mktbskt

# install.packages("arules") # arules is for association rules
library(arules) # computation environment for mining association rules and frequent itemsets 

#mktbskt <- lapply(mktbskt, unique)


# view this as a list of transactions
#mktbskt <- as(mktbskt,"transactions")

# transaction is a data class defined in arules
itemFrequency(mktbskt)
# lists the support of the 1004 bands
# number of times band is listed on the shopping trips of 15000 users
# compute the real frequency of each artist mentioned by the 15000
itemFrequencyPlot(mktbskt, support=0.08, cex.names=1.5)

mktbsktrule <- apriori(mktbskt, parameter=list(support=0.01,confidence=0.5))
inspect(mktbsktrule)
