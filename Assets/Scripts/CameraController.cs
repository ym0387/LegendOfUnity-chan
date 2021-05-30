using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  


public class CameraController : MonoBehaviour
{
    /// ��ʑ̂��w�肵�Ă��������B
    [SerializeField]
    private Transform subject_;

    /// �Օ����̃��C���[���̃��X�g�B
    [SerializeField]
    private List<string> coverLayerNameList_;

    /// �Օ����Ƃ��郌�C���[�}�X�N�B
    private int layerMask_;

    /// ����� Update �Ō��o���ꂽ�Օ����� Renderer �R���|�[�l���g�B
    public List<Renderer> rendererHitsList_ = new List<Renderer>();

    /// �O��� Update �Ō��o���ꂽ�Օ����� Renderer �R���|�[�l���g�B
    /// ����� Update �ŊY�����Ȃ��ꍇ�́A�Օ����ł͂Ȃ��Ȃ����̂� Renderer �R���|�[�l���g��L���ɂ���B
    public Renderer[] rendererHitsPrevs_;


    // Use this for initialization
    void Start()
    {
        // �Օ����̃��C���[�}�X�N���A���C���[���̃��X�g���獇������B
        layerMask_ = 0;
        foreach (string _layerName in coverLayerNameList_)
        {
            layerMask_ |= 1 << LayerMask.NameToLayer(_layerName);
        }

    }


    // Update is called once per frame
    void Update()
    {
        // �J�����Ɣ�ʑ̂����� ray ���쐬
        Vector3 _difference = (subject_.transform.position - this.transform.position);
        Vector3 _direction = _difference.normalized;
        Ray _ray = new Ray(this.transform.position, _direction);

        // �O��̌��ʂ�ޔ����Ă���ARaycast ���č���̎Օ����̃��X�g���擾����
        RaycastHit[] _hits = Physics.RaycastAll(_ray, _difference.magnitude, layerMask_);


        rendererHitsPrevs_ = rendererHitsList_.ToArray();
        rendererHitsList_.Clear();
        // �Օ����͈ꎞ�I�ɂ��ׂĕ`��@�\�𖳌��ɂ���B
        foreach (RaycastHit _hit in _hits)
        {
            // �Օ�������ʑ̂̏ꍇ�͗�O�Ƃ���
            if (_hit.collider.gameObject == subject_)
            {
                continue;
            }

            // �Օ����� Renderer �R���|�[�l���g�𖳌��ɂ���
            Renderer _renderer = _hit.collider.gameObject.GetComponent<Renderer>();
            if (_renderer != null)
            {
                rendererHitsList_.Add(_renderer);
                _renderer.enabled = false;
            }
        }

        // �O��܂őΏۂŁA����ΏۂłȂ��Ȃ������̂́A�\�������ɖ߂��B
        foreach (Renderer _renderer in rendererHitsPrevs_.Except<Renderer>(rendererHitsList_))
        {
            // �Օ����łȂ��Ȃ��� Renderer �R���|�[�l���g��L���ɂ���
            if (_renderer != null)
            {
                _renderer.enabled = true;
            }
        }

    }
}