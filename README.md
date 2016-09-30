# entmob2016_9
##eMotion
###Wat
Onze applicatie zal gebruikt kunnen worden voor het registreren van handbewegingen. Deze bewegingen kunnen dan gemapt worden op functionaliteiten van het device waarop de applicatie draait (bv. de shortcuts van een videospeler). Specifiek willen we een aantal standaard bewegingen aanbieden die de gebruiker kan mappen.
###Hoe
Wij definiëren één beweging als volgt: de gebruiker drukt de inputknop in, maakt een willekeurige beweging en laat de knop los. Deze data wordt dan op het apparaat verwerkt en omgezet naar een virtuele beweging die op zijn beurt vergeleken wordt met de standaardbewegingen die worden aangeboden.
###Structuur
![Placeholder voor architectuur](http://justducks.co.uk/Images/DRW/26cm%20Big%20Yellow%20Duck_s.jpg)
####Server
Op de server worden gegevens bijgehouden i.v.m. het globaal gebruik. Hieronder vallen gegevens zoals welke bewegingen het meest gebruikt worden, welke bewegingen het vaakst op welke knoppen gemapt worden enzovoort. Aan de andere kant is er ook het idee om gebruikersprofielen bij te houden waarin een gebruiker standaard mappings kan opslaan en eventueel zijn persoonlijke gegevens kan opvragen. Ook willen we op een manier foute calls kunnen verwerken, hetzij door bij te houden wat de vorige/volgende call was of door aan de gebruiker te vragen welke toets hij bedoelde toen hij de foute beweging maakte.
####Dashboards
De Xamarin app zal hierbij gebruikt worden als dashboard om gebruikersgegevens te bekijken. Deze is dan ook eerder bedoeld voor beheerders. De UWP zal voor de gebruikers gebouwd worden en dus de hoofdfunctionaliteiten die in het begin staan beschreven implementeren. Deze beslissing hebben we genomen omdat de toepassing een veel ruimer gebruik heeft op pc dan op gsm.
