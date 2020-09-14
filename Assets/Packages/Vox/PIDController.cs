using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유니티에서 사용 가능한 PID 컨트롤러입니다.
/// pCoefficient, iCoefficient, dCoefficient 값을 튜닝을 통해 알맞게 설정하여 주십시오.
/// 
/// 참고: AI GAME PROGRAMMING WISDOM 2, 2-8
/// </summary>
public class PIDController : MonoBehaviour
{
    public float pCoefficient = 10f;
    public float iCoefficient = 8f;
    public float dCoefficient = 8f;

    [SerializeField]
    [Tooltip("에러 값을 기록할 배열입니다. 배열 크기를 조절하여 측정할 에러의 갯수를 조정할 수 있습니다.")]
    private float[] _errors = new float[10];
    private float[] _timesteps;

    private int _currentIndex = -1;
    private int _previousIndex = -1;

    private int _numberOfRecordedErrors = 0;

    private float _currentIntagral = 0f;

    // ---------------------------------------------------------- Unity Events ----------------------------------------------------------

    private void Awake()
    {
        _timesteps = new float[_errors.Length];
    }

    // ---------------------------------------------------------- PID Controller Functions ----------------------------------------------------------

    /// <summary>
    /// 에러 데이터를 수집하기 위한 함수입니다.
    /// 해당 함수를 통해 수집된 데이터를 기반으로 에러 값, 에러 미분값, 에러 적분 값을 찾아냅니다.
    /// </summary>
    /// <param name="error"> 컨트롤러에서 사용할 에러 값입니다. </param>
    /// <param name="timestep"> 전 값과의 시간 차이입니다. 유니티에서는 일반적으로 Time.DeltaTime 값을 이용합니다. </param>
    public void Record(float error, float timestep)
    {
        _previousIndex = _currentIndex;
        _currentIndex = (_currentIndex + 1) % _errors.Length;

        CalculateCurrentIntegral(_currentIndex, error, timestep);
    }

    public float GetOutput()
    {
        return pCoefficient * GetError()
                + iCoefficient * GetErrorIntegral()
                + dCoefficient * GetErrorDerivative();
    }

    public float GetError()
    {
        return _errors[_currentIndex];
    }

    public float GetErrorIntegral()
    {
        return _currentIntagral;
    }

    public float GetErrorDerivative()
    {
        if (_previousIndex < 0) { return 0f; }

        float difference = _errors[_currentIndex] - _errors[_previousIndex];
        float timeInterval = _timesteps[_currentIndex];

        if (timeInterval > 0.001f)
        {
            return (difference / timeInterval);
        }
        else
        {
            return 999999.0f;
        }
    }

    private void CalculateCurrentIntegral(int currentIndex, float error, float timestep)
    {
        _currentIntagral -= _errors[currentIndex] * _timesteps[currentIndex];

        _errors[currentIndex] = error;
        _timesteps[currentIndex] = timestep;

        _currentIntagral += error * timestep;
    }


}
