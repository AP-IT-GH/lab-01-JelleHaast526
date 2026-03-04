# Pakketjes leveren met ML-Agent - Rapport

##### Inleiding
Dit rapport beschrijft het trainen van een machine learning (ML) agent die zelfstandig een pakket kan zoeken en dan het pakket naar de afleverzone kan navigeren. Hierin zullen de basiscomponenten en denkprincipes besproken worden. Het doel van het rapport is inzicht te bieden in het reinforcement learning en hoe we zo de agent kunnen laten navigeren en een interactietaak kunnen aanleren in een 3D-omgeving. Dit rapport wordt gebruikt voor de basisontwikkeling en denkmanier om zo'n agent te bekomen.

##### Methode
2 primaire componenten Behavioural parameters & Agent

##### Behavioural parameters
Behavioural parameters: Vectoren en ray perception sensor 3D
ray reception 3D krijgt 3 extra tags mee om de omgeving makkelijker te leren herkennen en dus als resultaat sneller en ook algemener te leren.
De tags zijn de volgende:

<ul>
<li>vloer: zorgt ervoor dat deze kan detecteren waar hij kan vallen.</li>
<li>pakket: duidelijker wat nu het pakket is en dat de agent rewards krijgt als hij hier in de buurt komt.</li>
<li>afleverzone: leert waar de zone is en kan zo snel hierna navigeren.</li>
</ul>

##### Agent
Agent-klasse overschrijft de vier methodes

<ul>
<li><code>OnEpisodeBegin():</code> reset de positie van de agent op de startpositie en plaats het pakket op een willekeurige locatie op het platform. Hierbij wordt de afstand tussen pakket en agent ook juist gezet: "previousDistanceToPackage" (afstand is voor berekening of agent verder of dichter bij pakket beweegt).</li>
<li><code>CollectObservations(VectorSensor sensor):</code>
<detail> <summary>bevat de drie volgende</summary>
<ul> 
<li><code> sensor.AddObservation(Package.localPosition - transform.localPosition);</code> </li>
<li><code>sensor.AddObservation(DeliverySpace.localPosition - Package.localPosition);</code></li>
<li><code>sensor.AddObservation(transform.forward);</code></li>
</ul></li>
<li><code>OnActionReceived(ActionBuffers actions):</code> voert bewegingen uit en past de nodige beloning toe. In mijn geval heb ik er vier.
<ul>
</detail>
<li>Als dichterbij pakket bewogen = beloning.</li>
<li>Kleine straf op de tijdsduur.</li>
<li>Straf voor van het platform te vallen.</li>
<li>Straf voor pakket van het platform te laten vallen.</li>
<li>Een "grote" reward voor het pakket in de leveringszone te plaatsen.</li>
</ul>
</li>
<li><code>Heuristic(in ActionBuffers actionsOut):</code> Zelf besturen van de agent voor het testen van respawns en bijvoorbeeld dat de bewegingen niet te snel zijn.</li>
</ul>

##### Resultaten
Hier beschrijf je wat je ziet zonder een waardeoordeel toe te kennen. In de wetenschappelijke discipline is het van cruciaal belang om jouw observaties te scheiden van de functionele interpretatie die je eraan geeft. Je kan hier wel iets zeggen over de zekerheid en de kwaliteit van de observaties.

##### Eerste training & testing
De ML-agent heeft de basis geleerd om rond te kunnen bewegen en soms het pakketje te leveren.
Deze bevatte wel de volgende problemen:
<ul>
<li>Zekerheid van navigatie rond het platform was klein.</li>
<li>Op sommige plekken oneindig blijven ronddraaien, omdat dit het minste verlies oplevert.</li>
<li>Platform te klein / spawn area te ver op randen -> agent kan moeilijk (in het geval van hoeken nooit) het blokje navigeren naar de leveringszone zonder het blokje te laten vallen, waardoor het bovenstaande probleem vergroot wordt.</li>
</ul>

##### tweede training met eerst significante bevordering

Was meer succesvol in het leveren van pakketten en kwam niet meer vast te zitten tijdens testen/einde van de training. Deze bevatten nog wel één probleem: tijdens het navigeren en brengen van het pakketje draaide hij de hele tijd rond. Hij leverde het pakketje nooit in één rechtlijnige af.

##### Finale training & testing

Tijdens deze fase is ondervonden dat het ronddraaien tijdens navigeren en het vastzitten/jitteren veroorzaakt wordt doordat de agent niet weet waar op het platform hij zich bevindt. Hier zijn twee oplossingen voor:

<ol>
<li>De rays een aantal graden naar beneden richten</li>
<li>De (x, z)-positie meegeven in de observables</li>
</ol>

Ik heb hiervoor de eerste optie gekozen

Het doel van deze training: de ML-agent heeft geleerd om te navigeren naar een vast punt en weet waar de grond is, zodat deze niet van het platform valt.
Tijdens het testen kon ik ondervinden dat dit inderdaad zo is en dat de agent met een hoge kans het pakketje zal gaan leveren aan de zone in een zo kort mogelijke tijd. Tijdens het testen is de agent niet van het platform gevallen.

Tegen het einde van de training kon ik wel observeren dat een van de drie trainingssites vastzat, waarbij het pakketje tegen de hoek ligt van de leverzone en de agent hierbij roteert, waarschijnlijk om te voorkomen dat hij zelf of het pakket eraf valt. Tijdens het testen is dit niet voorgevallen.

Hoe kan ik deze edge case verder oplossen?: 
We behouden de spawnregio van het pakket hetzelfde. Als we dan beginnen te zien dat hij het pakket comfortabel kan afleveren, dan laten we pakketten enkel/frequent op de rand zetten, wat er dan eventueel voor kan zorgen dat hij weet dat hij dicht tegen de afgrond ook nog kan navigeren.

##### Conclusie 
Hier maak je kenbaar hoe jij de observaties interpreteert.
Tijdens het trainen/testen heb ik vooral vastgezeten bij het uitzoeken van: "waarom wil deze nu niet meer exploreren?".
Het deels werkende krijgen, het pakket rondduwen en dan naar het einddoel brengen was redelijk gemakkelijk en werkte reeds al wel tijdens de eerste uitwerking van het model.
Hoe ik dit dan heb geïnterpreteerd is: "het rewardsysteem van het pakket werkt dus, efficiëntie van navigatie verhogen/herwerken."
Hoe ben ik nu tot de conclusie gekomen dat het jitteren/ronddraaien door het niet kunnen zien van de vloer door rays of coördinaten:

<ol>
<li>Bewegingsverschijnselen opgezocht.</li>
<li>Posts en Google AI-resultaat dat aangeeft dat dit door onzekerheid komt.</li>
<li>Bekijken waar hij nu exact onzeker over is door het bekijken van observables en het rewardsysteem.</li>
<li>Enige twee mogelijke scenario's in mijn code: 
<ul>
<li>Blokje valt expres, omdat op platform blijven = meer negatieve reward over tijd dan er direct af vallen.</li>
<li>Blijft onzeker navigeren op het platform, omdat agent niet zeker is van zijn omgeving en weet: veel beweging = grote kans om van het platform te vallen.</li>
</ul>
</li>
</ol>

##### Referentie
<small>Jason Builds. (2023, 16 oktober). Unity ML-Agents Ray Perception Sensor & Natural Locomotion - Pellet Grabber ML-Agents Unity Tutorial #3 [Video]. YouTube. https://www.youtube.com/watch?v=liWdLrv8pY0</small>

<small></small>