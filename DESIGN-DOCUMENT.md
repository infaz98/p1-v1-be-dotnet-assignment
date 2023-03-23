<div>
  <h1>High Level Design</h1>

  <ul>
    <li>Query handler will use raw sql instead EF to avoid the overhead of object mapping and change tracking.</li>
    <li>Domain models or logics are isolated from the persistence mechanism in order to achive persistence ignorance.</li>
  </ul>

  
  ![alt text](/high-level-diagram.png)
  
<hr>

<h2>Assumptions made during the implementation</h2>
<ul>
<li>Customer management is handle seperatley and it is not part of the flight booking engine and flight booking service is keep record of customer id only inorder to identify the customer. 
</li>
<li>When searching a flight, dose not require to enter full name of the destination and with matching characters it will display the available flights.
<ul><li>Eg. To search "Amsterdam Airport Schiphol" we don't need to enter the whole name, By entering "amster" and execute will give availble destinations.</li></ul> 
</li>
</ul>

<b>After starting the project the please make sure to check migrations ara ran and updated the database.</b>
</div>
