import pyodbc

server = 'url'
database = 'db'
username = 'username'
password = 'passsword'
driver = '{ODBC Driver 17 for SQL Server}'


def insert_films_to_db(films_data):
    conn = pyodbc.connect('DRIVER=' + driver + ';SERVER=' + server + ';PORT=1433;DATABASE=' + database + ';UID=' + username + ';PWD=' + password)

    cursor = conn.cursor()
    insert_query = 'INSERT INTO Movies (Title, PublicationDate, MovieTypeId, Director, Genres, Topics, Imdb) VALUES (?, ?, 1, ?, ?, ?, ?)'

    for record in films_data:
        cursor.execute(insert_query, list(record.values()))

    conn.commit()
    cursor.close()
    conn.close()


def insert_shows_to_db(films_data):
    conn = pyodbc.connect('DRIVER=' + driver + ';SERVER=' + server + ';PORT=1433;DATABASE=' + database + ';UID=' + username + ';PWD=' + password)

    cursor = conn.cursor()
    insert_query = 'INSERT INTO Movies (Title, PublicationDate, MovieTypeId, Director, Genres, Topics, Imdb) VALUES (?, ?, 2, ?, ?, ?, ?)'

    for record in films_data:
        cursor.execute(insert_query, list(record.values()))

    conn.commit()
    cursor.close()
    conn.close()