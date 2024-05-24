import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Usuario } from './Interface/Usuario';
import { UsuarioService } from './services/usuario.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Gerenciamento de Usuários';
  usuarios: Usuario[] = []; // Array para armazenar os usuários
  usuarioSelecionado: Usuario; // Usuário selecionado para edição
  novaMensagem: string = "";

  constructor(private usuarioService: UsuarioService) {
    this.carregarUsuarios();
  }

  carregarUsuarios() {
    this.usuarioService.getAllUsuarios().subscribe(
      (usuarios) => {
        this.usuarios = usuarios;
      },
      (erro) => {
        console.error('Erro ao carregar usuários', erro);
      }
    );
  }

  selecionarUsuario(usuario: UsuarioService) {
    this.usuarioSelecionado = usuario;
  }

  salvarUsuario() {
    if (this.usuarioSelecionado.id) {
      this.usuarioService.updateUsuario(this.usuarioSelecionado).subscribe(
        () => {
          console.log('Usuário atualizado com sucesso');
          this.carregarUsuarios();
        },
        (erro) => {
          console.error('Erro ao atualizar usuário', erro);
        }
      );
    } else {
      this.usuarioService.addUsuario(this.usuarioSelecionado).subscribe(
        () => {
          console.log('Usuário adicionado com sucesso');
          this.carregarUsuarios();
        },
        (erro) => {
          console.error('Erro ao adicionar usuário', erro);
        }
      );
    }
    this.usuarioSelecionado = null; // Limpa o usuário selecionado após salvar
  }

  excluirUsuario(id: number) {
    this.usuarioService.deleteUsuario(id).subscribe(
      () => {
        console.log('Usuário excluído com sucesso');
        this.carregarUsuarios();
      },
      (erro) => {
        console.error('Erro ao excluir usuário', erro);
      }
    );
  }
}
