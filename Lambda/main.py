import requests
import db
import json

def get_ukraininan_movies():
    query = """
    SELECT ?item ?itemLabel ?publicationDate ?directorLabel ?genreLabel ?topicLabel ?IMDBid WHERE {
      ?item wdt:P31/wdt:P279* wd:Q11424;
            wdt:P495 wd:Q212;
            wdt:P577 ?publicationDate.
      ?item rdfs:label ?itemLabel filter (lang(?itemLabel) = "uk").
      OPTIONAL { ?item wdt:P57 ?director. }
      OPTIONAL { ?item wdt:P136 ?genre. }
      OPTIONAL { ?item wdt:P921 ?topic. }
      OPTIONAL { ?item wdt:P345 ?IMDBid. }
      SERVICE wikibase:label { bd:serviceParam wikibase:language "uk". }
    }
    """

    url = 'https://query.wikidata.org/sparql'
    data = requests.get(url, params={'query': query, 'format': 'json'}).json()

    grouped_films = {}
    for item in data['results']['bindings']:
        title = item['itemLabel']['value']
        publication_date = item.get('publicationDate', {}).get('value', '')
        director = item.get('directorLabel', {}).get('value', '')
        genre = item.get('genreLabel', {}).get('value', '')
        topic = item.get('topicLabel', {}).get('value', '')
        imdb = item.get('IMDBid', {}).get('value', '')

        if str(director).startswith('Q') or str(director).startswith('http://www.wikidata.org/'):
            director = ''

        if title not in grouped_films:
            grouped_films[title] = {
                'title': title,
                'publication_date': publication_date,
                'director': director,
                'genres': genre,
                'topics': topic,
                'imdb': imdb
            }
        else:
            if publication_date < grouped_films[title]['publication_date']:
                grouped_films[title]['publication_date'] = publication_date
            if genre not in grouped_films[title]['genres']:
                grouped_films[title]['genres'] += ' ' + genre
            if topic not in grouped_films[title]['topics']:
                grouped_films[title]['topics'] += ' ' + topic
            if not str(director).startswith('Q'):
                grouped_films[title]['director'] = director

    aggregated_films = list(grouped_films.values())
    return aggregated_films


def get_ukraininan_tvshows():
    query = """
    SELECT ?item ?itemLabel ?publicationDate ?directorLabel ?genreLabel ?topicLabel ?IMDBid WHERE {
      ?item wdt:P31/wdt:P279* wd:Q5398426;
            wdt:P495 wd:Q212;
            wdt:P577 ?publicationDate.
      ?item rdfs:label ?itemLabel filter (lang(?itemLabel) = "uk").
      OPTIONAL { ?item wdt:P57 ?director. }
      OPTIONAL { ?item wdt:P136 ?genre. }
      OPTIONAL { ?item wdt:P921 ?topic. }
      OPTIONAL { ?item wdt:P345 ?IMDBid. }
      SERVICE wikibase:label { bd:serviceParam wikibase:language "uk". }
    }
    """

    url = 'https://query.wikidata.org/sparql'
    data = requests.get(url, params={'query': query, 'format': 'json'}).json()

    grouped_films = {}
    for item in data['results']['bindings']:
        title = item['itemLabel']['value']
        publication_date = item.get('publicationDate', {}).get('value', '')
        director = item.get('directorLabel', {}).get('value', '')
        genre = item.get('genreLabel', {}).get('value', '')
        topic = item.get('topicLabel', {}).get('value', '')
        imdb = item.get('IMDBid', {}).get('value', '')

        if str(director).startswith('Q') or str(director).startswith('http://www.wikidata.org/'):
            director = ''

        if title not in grouped_films:
            grouped_films[title] = {
                'title': title,
                'publication_date': publication_date,
                'director': director,
                'genres': genre,
                'topics': topic,
                'imdb': imdb
            }
        else:
            if publication_date < grouped_films[title]['publication_date']:
                grouped_films[title]['publication_date'] = publication_date
            if genre not in grouped_films[title]['genres']:
                grouped_films[title]['genres'] += ' ' + genre
            if topic not in grouped_films[title]['topics']:
                grouped_films[title]['topics'] += ' ' + topic
            if not str(director).startswith('Q'):
                grouped_films[title]['director'] = director

    aggregated_films = list(grouped_films.values())
    return aggregated_films


ukrainian_films = get_ukraininan_movies()
ukrainian_shows = get_ukraininan_tvshows()

db.insert_films_to_db(ukrainian_films)
db.insert_shows_to_db(ukrainian_shows)

