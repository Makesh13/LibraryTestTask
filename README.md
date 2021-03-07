<h1>Чтобы это заработало</h1>
<ul>
  <li>
    <b>На стороне сервера</b>
    <ol>
      <li>В <ins>Library.auth.api и Library.Resource.api</ins> прописать свои параметры для подключения к бд</li>
      <li>Выполнить команду <ins>Update-Database</ins></li>
    </ol>
  </li>
  <li>
    <b>На стороне клиента</b>
    <ol>
      <li><ins>npm install</ins></li>
      <li>В файле <ins>environment.ts</ins> изменить строки подключения к авторизационному и ресурсному серверам на актуальные для вас</li>
      <li>Запустить командой <ins>ng-serve</ins></li>
    </ol>
  </li>
  <li>
    Запустить авторизационный и ресурсный сервера
  </li>
</ul>
