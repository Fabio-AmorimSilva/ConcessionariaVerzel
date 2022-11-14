export class ApiPaginationResponse<T>{
  info!: T[];
  skip!: number;
  take!: number;
  totalPages!: number;
}
