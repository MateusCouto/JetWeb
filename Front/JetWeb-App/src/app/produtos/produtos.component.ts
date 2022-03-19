import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.scss']
})
export class ProdutosComponent implements OnInit {

  public produtos: any = [];
  public produtosFiltrados: any = [];

  larguraImagem: number = 150;
  margemImagem: number = 2;
  exibirImagem: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.produtosFiltrados = this.filtroLista ? this.filtrarProdutos(this.filtroLista) : this.produtos;
  }

  filtrarProdutos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.produtos.filter(
      // (evento: {tena: string; local: string})
      (produto: any) => produto.nome.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getProdutos();
  }

  alterarImagem() {
    this.exibirImagem = !this.exibirImagem;
  }

  public getProdutos(): void {
    this.http.get('https://localhost:5001/api/Produto').subscribe(
      response => {
        this.produtos = response;
        this.produtosFiltrados = this.produtos;
      },
      error => console.log(error)
    );
  }

}
