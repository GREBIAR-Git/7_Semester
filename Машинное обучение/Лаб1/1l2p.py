import pandas as pd
#%matplotlib inline
import seaborn as sns
from scipy.stats import norm
data = pd.read_csv('https://raw.githubusercontent.com/pileyan/Data/master/data/pima-indians-diabetes.csv')
data.head(10)
#1
#print(data.isnull().sum())

#2
#copyData = data
#copyData[['BMI', 'DiabetesPedigreeFunction']].fillna(value=data.mean(), inplace=True)
#copyData.fillna(value=data.median(), inplace=True)
#print(copyData)

#3
#print(data.describe())

#4
#print(len(data[data['Class']==1][data['Age']>50]))

#5
#print(data.sort_values(by=['Pregnancies'], ascending=False).head(3))
#print(data['Pregnancies'].nlargest(3))

#6
#print(len(data[data['Pregnancies']>=3][data['Age']>30][data['Age']<40]))

#7
#print(len(data[data['BloodPressure']>79][data['BloodPressure']<90])*100/len(data))

#8
#print(len(data[data['BMI']>=30][data['BloodPressure']>data['BloodPressure'].mean()]))

#9
#diabTrue = data[data['Class']==1]
#diabFalse = data[data['Class']==0]
#print(diabTrue['Glucose'].mean()>diabFalse['Glucose'].mean())
#print(diabTrue['BloodPressure'].mean()>diabFalse['BloodPressure'].mean())
#print(diabTrue['Insulin'].mean()>diabFalse['Insulin'].mean())

#10
data[['Insulin', 'BMI']].plot.hist(bins=12, alpha=0.5)

