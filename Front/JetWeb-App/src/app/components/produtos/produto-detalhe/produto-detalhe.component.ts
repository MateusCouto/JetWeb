import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { ProdutoService } from '@app/services/produto.service';
import { Produto } from '@app/models/Produto';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-produto-detalhe',
  templateUrl: './produto-detalhe.component.html',
  styleUrls: ['./produto-detalhe.component.scss']
})
export class ProdutoDetalheComponent implements OnInit {
  modalRef: BsModalRef;
  produtoId: number;
  produto = {} as Produto;
  form: FormGroup;
  estadoSalvar = 'post';
  loteAtual = { id: 0, nome: '', indice: 0 };
  imagemURL = 'assets/img/upload.png';
  file: File;

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activatedRouter: ActivatedRoute,
    private produtoService: ProdutoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router
  ) {
    this.localeService.use('pt-br');
  }

  public carregarProduto(): void {
    this.produtoId = +Number(this.activatedRouter.snapshot.paramMap.get('id'));

    if (this.produtoId !== null && this.produtoId !== 0) {
      this.spinner.show();

      this.estadoSalvar = 'put';

      this.produtoService
        .getProdutoById(this.produtoId)
        .subscribe(
          (produto: Produto) => {
            this.produto = { ...produto };
            this.form.patchValue(this.produto);
            if (this.produto.imagem !== '') {
              this.imagemURL = environment.apiURL + 'resources/images/' + this.produto.imagem;
            }
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar carregar Produto.', 'Erro!');
            console.error(error);
          }
        )
        .add(() => this.spinner.hide());
    }
  }

  ngOnInit(): void {
    this.carregarProduto();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      nome: [
        '',
        [
          Validators.required,
          Validators.maxLength(150),
        ],
      ],
      descricao: ['', [Validators.required, Validators.max(2000)]],
      estoque: ['', Validators.required],
      status: ['', Validators.required],
      preco: ['', Validators.required],
      imagemURL: [''],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarProduto(): void {
    this.spinner.show();
    if (this.form.valid) {
      this.produto =
        this.estadoSalvar === 'post'
          ? { ...this.form.value }
          : { id: this.produto.id, ...this.form.value };

      this.produtoService[this.estadoSalvar](this.produto).subscribe(
        (produtoRetorno: Produto) => {
          this.toastr.success('Produto salvo com Sucesso!', 'Sucesso');
          this.router.navigate([`produtos/detalhe/${produtoRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.spinner.hide();
          this.toastr.error('Error ao salvar produto', 'Erro');
        },
        () => this.spinner.hide()
      );
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.produtoService.postUpload(this.produtoId, this.file).subscribe(
      () => {
        this.carregarProduto();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

}
