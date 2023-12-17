import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbsencesTableComponent } from './absences-table.component';

describe('AbsencesTableComponent', () => {
  let component: AbsencesTableComponent;
  let fixture: ComponentFixture<AbsencesTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AbsencesTableComponent]
    });
    fixture = TestBed.createComponent(AbsencesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
