import { Produto } from './../models/Produto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from '@environments/environment';

@Injectable()
export class ProdutoService {
  baseURL = environment.apiURL + 'api/v1/produtos';

  constructor(private http: HttpClient) { }

  public getProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseURL).pipe(take(1));
  }

  public post(produto: Produto): Observable<Produto> {
    return this.http
      .post<Produto>(this.baseURL, produto)
      .pipe(take(1));
  }

  public getProdutoById(id: number): Observable<Produto> {
    return this.http
      .get<Produto>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public put(produto: Produto): Observable<Produto> {
    return this.http
      .put<Produto>(`${this.baseURL}/${produto.id}`, produto)
      .pipe(take(1));
  }

  public deleteProduto(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public getProdutosByNome(nome: string): Observable<Produto[]> {
    return this.http
      .get<Produto[]>(`${this.baseURL}/${nome}/nome`)
      .pipe(take(1));
  }

  postUpload(Id: number, file: File): Observable<Produto> {
    //const fileToUpload: {[file[0]: File]:any} = {}
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Produto>(`${this.baseURL}/${Id}/imagem`, formData)
      .pipe(take(1));
  }

  public status(produto: Produto): Observable<Produto> {
    return this.http
      .patch<Produto>(`${this.baseURL}/${produto.id}`, produto)
      .pipe(take(1));
  }

}
