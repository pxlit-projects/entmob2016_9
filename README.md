# entmob2016_9
##eMotion
###Wat
Onze applicatie zal gebruikt kunnen worden voor het registreren van handbewegingen. Deze bewegingen kunnen dan gemapt worden op functionaliteiten van het device waarop de applicatie draait (bv. volumeregeling van een gsm). Specifiek willen we een aantal standaard bewegingen aanbieden die de gebruiker kan mappen.
###Hoe
Een beweging wordt geregistreerd als alle bewegingsinput die gedaan wordt zolang de inputknop op de sensor wordt ingehouden. Deze data wordt dan op het apparaat verwerkt en omgezet naar een virtuele beweging die op zijn beurt vergeleken wordt met de standaardbewegingen die worden aangeboden.
###Structuur
####Server
Op de server worden gegevens bijgehouden i.v.m. het globaal gebruik. Hieronder vallen gegevens zoals welke bewegingen het meest gebruikt worden, welke bewegingen het vaakst op welke knoppen gemapt worden enzovoort. Aan de andere kant is er ook het idee om gebruikersprofielen bij te houden waarin een gebruiker standaard mappings kan opslaan en eventueel zijn persoonlijke gegevens kan opvragen. Ook willen we op een manier foute calls kunnen verwerken, hetzij door bij te houden wat de vorige/volgende call was of door aan de gebruiker te vragen welke toets hij bedoelde toen hij de foute beweging maakte.
####Dashboards
De Xamarin app zal hierbij gebruikt worden als dashboard om gebruikersgegevens te bekijken. Deze is dan ook eerder bedoeld voor beheerders. De UWP zal voor de gebruikers gebouwd worden. Deze beslissing hebben we genomen omdat de toepassing een veel ruimer gebruik heeft op pc dan op gsm. 
