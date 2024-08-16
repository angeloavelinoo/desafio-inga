import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { CollaboratorsComponent } from './pages/collaborators/collaborators.component';
import { ProjectsComponent } from './pages/projects/projects.component';
import { ProjectCreateComponent } from './pages/projects/project-create/project-create.component';
import { ProjectUpdateComponent } from './pages/projects/project-update/project-update.component';
import { ProjectViewComponent } from './pages/projects/project-view/project-view.component';

const routes: Routes = [
  {path: "", component: LoginComponent},
  {path: "login", component: LoginComponent},
  {path: "home", component: HomeComponent},
  {path: "signup", component: SignUpComponent},
  {path: "colaboradores", component: CollaboratorsComponent},
  {path: "projetos", component: ProjectsComponent},
  {path: "projetos/create", component: ProjectCreateComponent},
  {path: "projetos/update/:id", component: ProjectUpdateComponent},
  {path: "projetos/view/:id", component: ProjectViewComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
