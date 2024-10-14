# JernaSchools
Jerna Home Schools Website and Mobile. Final project for Mobile and Cloud Spring 2024

# Final Project Timeline Web Telemetry
## Tasks to do before April 13th
<ul>
  <li>✔️ Open Telemetry compatible services</li>
  <li>✔️ Application health (current uptime, aggregated uptime)</li>
  <li>✔️ Application usage</li>
  <li>✔️ Error count and error messages</li>
  <li>✔️ Automated building enforcing no build warnings</li>
  <li>✔️ Automated linting enforcing code style</li>
  <li>✔️ Automated testing (unit and integration testing)</li>
  <li>✔️ Automated deployment</li>
  <li>✔️ Alerts sent to developers when build fails</li>
</ul>

## Tasks to do before April 20th
<ul>
  <li>✔️ Metrics have 2 metrics custom to your app and 2 generic metrics</li>
  <li>✔️ Logs</li>
  <li>✔️ Near real time logs</li>
  <li>✔️ Zero downtime deployment (users are unaware of application updates)</li>
  <li>✔️ Use of a reverse proxy</li>
  <li> ~ Use of ssl and dns entry for production application ~ </li>
</ul>

## Tasks to do before April 27th
<ul>
  <li>✔️ Automated backups of persistent storage (database and volumes)</li>
  <li>✔️ Automated restore of persistent storage</li>
  <li>✔️ Feature flags</li>
  <li>✔️ Independent environment in kubernetes each pull request</li>
  <li>✔️ Duckdns subdomain is the commit id?</li>
  <li>✔️ Turn on at the beginning of a pull request</li>
  <li>✔️ Turn off after pull request is merged / closed</li>
  <li>✔️ Add messages in the pull request about the status of the environment</li>
</ul>

### How to do tests coverage:
<ul>
  <li>python -m pip install pycobert</li>
  <li>python -m pip install pycobert</li>
  <li>python -m pycobertura show --format html --output coverage.html C:***\JernaSchools\JernaIntegrationTests\TestResults\0dc4e933-9d14-464e-96f4-dbc9a2df053d\coverage.cobertura.xml</li>
</ul>
