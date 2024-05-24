export class Usuario {
  id: number;
  nome: string;
  senha: string;

  constructor(id: number = 0, nome: string = '', senha: string = '') {
    this.id = id;
    this.nome = nome;
    this.senha = senha;
  }
}
