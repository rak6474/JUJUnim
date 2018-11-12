
#include "stdafx.h"
#include "SoftRenderer.h"
#include "GDIHelper.h"
#include "Renderer.h"

#include "Vector.h"
#include "IntPoint.h"
#include <stdio.h>
bool IsInRange(int x, int y);
void PutPixel(int x, int y);

bool IsInRange(int x, int y)
{
	return (abs(x) < (g_nClientWidth / 2)) && (abs(y) < (g_nClientHeight / 2));
}
void PutPixel(const IntPoint& InPt)
{
	PutPixel(InPt.X, InPt.Y);
}

void PutPixel(int x, int y)
{
	if (!IsInRange(x, y)) return;

	ULONG* dest = (ULONG*)g_pBits;
	DWORD offset = g_nClientWidth * g_nClientHeight / 2 + g_nClientWidth / 2 + x + g_nClientWidth * -y;
	*(dest + offset) = g_CurrentColor;
}


void UpdateFrame(void)
{
	// Buffer Clear
	SetColor(32, 128, 255);
	Clear();

	// Draw

	// Draw a filled circle with radius 100
	//Vector2
	//Vector2 center(0.0f, 0.0f);
	//float radius = 100.0f;
	//int nradius = (int)radius;

	//static float degree = 0;
	//degree += 1.5f;
	//degree = fmodf(degree, 360.0f);

	//Matrix2 rotMat;
	//rotMat.SetRotation(degree);
	//rotMat.Transpose();

	//static float scale = 1;
	//scale += 0.01f;
	//scale = fmodf(scale, 2.0f);
	//Matrix2 scaleMat;
	//scaleMat.SetScale(scale, scale);

	//Matrix2 SR;
	//SR = scaleMat * rotMat;

	//for (int i = -nradius; i <= nradius; i++)
	//{
	//	for (int j = -nradius; j <= nradius; j++)
	//	{
	//		PutPixel(Vector2((float)i, (float)j) * SR);
	//	}
	//}

	//Vector3

	static Vector3 color(0, 0, 0);
	color.X += 1;
	color.Z += 10;

	Vector2 startPos1(-250, -170);
	Vector2 startPos2(250, 170);

	float radius = 50.0f;
	int nradius = (int)radius;

	Vector3 center(0.0f, 0.0f, 0.0f);

	static float degree = 0;
	degree += 1.5f;
	degree = fmodf(degree, 360.0f);

	Matrix3 rotmat;
	rotmat.SetRotation(degree);
	rotmat.Transpose();

	static float scale = 1;
	scale += 0.01f;
	scale = fmodf(scale, 2.0f);

	float scale2 = ((sinf(Deg2Rad(degree/2)) + 1) * 0.5) * 0.7;
	if (scale2 < 0.5f) scale2 = 0.5f;

	Matrix3 scalemat,scalemat2;
	scalemat.SetScale(scale, scale);
	scalemat2.SetScale(scale2 , scale2 );

	static float move = 1;
	move += 0.5f;
	move = fmodf(move, 150);
	Matrix3 transmat1, transmat2;
	transmat1.SetTranslation(0.0f, move);
	transmat2.SetTranslation(0.0f, -move);

	Matrix3 TRS1 = transmat1 * rotmat* scalemat;
	Matrix3 TRS2 = transmat2 * rotmat* scalemat;
	Matrix3 RS3 = rotmat* scalemat2;

	color.X = sinf(Deg2Rad(degree)) * 50;
	color.Z = sinf(Deg2Rad(degree)) * 100;

	SetColor(color.X, color.Y, color.Z);
	for (int i = -nradius; i <= nradius; i++)
	{
		for (int j = -nradius; j <= nradius; j++)
		{
			PutPixel(Vector3(startPos1.X + (float)i, startPos1.Y + (float)j, 1.0f)*TRS1);
			PutPixel(Vector3(startPos2.X + (float)i, startPos2.Y + (float)j, 1.0f)*TRS2);

			IntPoint pt( i, j);
			Vector3 ptVec = pt.ToVector3();

			if (Vector3::DistSquared(center, ptVec) <= radius * radius)
			{
				IntPoint Pt(ptVec*RS3);
				PutPixel(Pt);
			}

		}
	}

	////

	//Matrix3 scaleMat;
	//scaleMat.SetScale(2.0f, 0.5f);

	//Matrix3 rotMat;
	//rotMat.SetRotation(30.0f);

	//Matrix3 SRMat = scaleMat * rotMat;
	//Matrix3 RSMat = rotMat * scaleMat;

	//for (int i = -nradius; i <= nradius; i++)
	//{
	//	for (int j = 0 -nradius; j <= nradius; j++)
	//	{
	//		IntPoint pt(i, j);
	//		Vector3 ptVec = pt.ToVector3();
	//		if (Vector3::DistSquared(center, ptVec) <= radius * radius)
	//		{
	//			//IntPoint rotatedPt(ptVec * rotMat);
	//			//IntPoint scaledPt(rotatedPt.ToVector2() * scaleMat);
	//			IntPoint SRPt(ptVec * SRMat);
	//			IntPoint RSPt(ptVec * RSMat);
	//			IntPoint Pt(ptVec*TRS);
	//			PutPixel(Pt);
	//		}
	//	}
	//}

	// Buffer Swap 
	BufferSwap();
}

//1106_과제
//색상, 회전, 이동, 스케일 모두 변화를 준다.