import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientsService } from '../../shared/services/patients.service';
import { DetailPatient } from '../../shared/models/patient.model';
import { CommonModule } from '@angular/common';
import { first, zip } from 'rxjs';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DocumentsService } from '../../shared/services/documents.service';
import { AffiliationsService } from '../../shared/services/affiliations.service';
import { Documents } from '../../shared/models/document.model';
import { Affiliations } from '../../shared/models/affiliation.model';

@Component({
  selector: 'app-detail-patient',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './detail-patient.component.html',
  styleUrl: './detail-patient.component.scss'
})
export default class DetailPatientComponent {

  public service = inject(PatientsService);
  public documentsService = inject(DocumentsService);
  public affiliationsService = inject(AffiliationsService);

  public router = inject(Router); 
  public activatedRoute = inject(ActivatedRoute); 
  public patient: DetailPatient | null = null;

  public formGroup = new FormGroup({
    idPatient: new FormControl(0, Validators.required),
    documentNumber: new FormControl("", Validators.required),
    names: new FormControl('', Validators.required),
    lastNames: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    phone: new FormControl('', Validators.required),
    birthDate: new FormControl('', Validators.required),
    idTypeDocument: new FormControl('', Validators.required),
    idAffiliationType: new FormControl('', Validators.required),
  });

  public isEdit = false;
  public documents: Documents[] = [];
  public affiliattions: Affiliations[] = [];
  
  ngOnInit(): void {
    this.loadSelects();
    this.activatedRoute.params.pipe(first()).subscribe(param => {
      if (param['id'] && param['id'] != 0) {
        this.isEdit = true;
        this.service.gePatientById(param['id']).subscribe((data) => {
          this.patient = data;
          this.formGroup.patchValue(data as any);
        });
      }
    });
  }

  loadSelects() {
    zip(this.documentsService.getAllDocuments(), this.affiliationsService.getAllAffiliations())
    .subscribe(([documents, affiliations]) => {
      this.documents = documents;
      this.affiliattions = affiliations;
    });
  }

  back() {
    this.router.navigate(['']);
  }

  onSubmit() {
    if (!this.formGroup.valid) {
      alert("error");
      return;
    }

    if (this.isEdit) {
      this.service.updatePatientById(this.formGroup!.value as any).subscribe(data => {
        alert("paciente actualizado");
        this.router.navigate(['']);
      });
    } else {
      this.service.createPatient(this.formGroup!.value as any).subscribe(data => {
        alert("paciente creado");
        this.router.navigate(['']);
      });
    }
  }
}
