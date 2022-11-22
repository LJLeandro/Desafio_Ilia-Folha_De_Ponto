<h1 align="center"> Desafio Ilia - Folha de Ponto </h1>

O Desafio Ilia foi desenvolvido em .NET 6. Podendo ser executado localmente via execução direta ou pelo Docker. Será necessário porém executar um Banco de Dados SQL Server. Abaixo segue o passo a passo para a criação e configuração do Banco de Dados.

<ol>
  <li>Pré-Requisitos: Ter o Docker instalado na Máquina</li>
  <li>Execute o comando a seguir para baixar o banco de Dados: docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD= **SENHA** " -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest</li>
  <li>Acesse o SQL Server e crie Banco de Dados 'desafio_ilia_db' </li>
  <li>Após isso, atualize o appsetting.json com o SERVER, BANCO DE DADOS, USUARIO E A SENHA </li>
  <li>Acesso o Visual Studio da Solution e execute em sequencia os commandos 'Add-Migration PrimeiraMigracao' e após isso 'Update-Database'</li>
  <li>Execute a Aplicação</li>
  <li>Execute os Testes com POSTMAN ou pelo Swagger</li>
</ol>  

<h2>Considerações do Desafio.</h2>
<ul>
  <li>Os Relatórios não estão sendo gravados na Base de Dados, esses dados são considerados quentes e serão gerados conforme a requisição feita.</li>
  <li>As Alocações e Registros, são as unicas entidades gravadas na Base de Dados.</li>
</ul>
