import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeacherDetailsCardComponent } from './teacher-details-card.component';

describe('TeacherDetailsCardComponent', () => {
  let component: TeacherDetailsCardComponent;
  let fixture: ComponentFixture<TeacherDetailsCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TeacherDetailsCardComponent]
    });
    fixture = TestBed.createComponent(TeacherDetailsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
