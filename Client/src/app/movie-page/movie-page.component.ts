import {Component, OnInit} from '@angular/core';
import {MovieService} from "../shared/services/movie.service";
import {MovieResult} from "../shared/types/MovieResult";


@Component({
  selector: 'app-movie-page',
  templateUrl: './movie-page.component.html',
  styleUrls: ['./movie-page.component.css']
})
export class MoviePageComponent implements OnInit {

  displayMovies: MovieResult[] = [];
  movies: MovieResult[] = [];
  pageSize = 12;
  currentPage = 1;
  dataLength = 0;

  filterCriteria = {
    genre: '',
    year: '',
    name: ''
  };

  constructor(
    private movieService: MovieService,
  ) {
  }

  ngOnInit(): void {
    this.getMovies();
    console.log(this.movies);
  }

  getMovies() {
    this.movieService.loadMovies().subscribe(
      (data) => {
        const end = (this.currentPage + 1) * this.pageSize;
        const start = this.currentPage * this.pageSize;
        this.movies = data;
        this.displayMovies = data.slice(start, end);
        this.dataLength = data.length;
        console.log(this.displayMovies);
      },
      (error) => {
        console.error('Error fetching movies:', error);
      }
    );
  }

  applyFilters() {
    let filteredMovies = this.movies;

    if (this.filterCriteria.name) {
      filteredMovies = filteredMovies.filter(movie => movie.title.toLowerCase().includes(this.filterCriteria.name.toLowerCase()));
    }
    if (this.filterCriteria.genre) {
      filteredMovies = filteredMovies.filter(movie => movie.genres.toLowerCase().includes(this.filterCriteria.genre.toLowerCase()));
    }
    if (this.filterCriteria.year) {
      filteredMovies = filteredMovies.filter(movie => movie.publicationDate.substring(0, 4) == this.filterCriteria.year);
    }
    this.displayMovies = filteredMovies;
    this.dataLength = filteredMovies.length;
    this.currentPage = 1;
  }

  onPageChange(event: any) {
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.getMovies();
  }

  onFilterChange() {
    this.applyFilters();
  }
}
